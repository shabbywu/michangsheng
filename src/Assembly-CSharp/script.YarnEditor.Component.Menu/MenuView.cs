using System;
using Fungus;
using UnityEngine.Events;
using Yarn.Unity;

namespace script.YarnEditor.Component.Menu;

public class MenuView : DialogueViewBase
{
	public Action<int> OnOptionSelected;

	private bool showUnavailableOptions;

	public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		MenuDialog menuDialog = MenuDialog.GetMenuDialog();
		foreach (DialogueOption option in dialogueOptions)
		{
			if (option.IsAvailable || showUnavailableOptions)
			{
				menuDialog.SetActive(state: true);
				menuDialog.AddOption(option.Line.Text.Text, interactable: true, hideOption: false, null);
				((UnityEventBase)menuDialog.CachedButtons[menuDialog.NowOption].onClick).RemoveAllListeners();
				((UnityEvent)menuDialog.CachedButtons[menuDialog.NowOption].onClick).AddListener((UnityAction)delegate
				{
					OptionViewWasSelected(option);
					menuDialog.Clear();
					menuDialog.SetActive(state: false);
				});
			}
		}
		OnOptionSelected = onOptionSelected;
	}

	private void OptionViewWasSelected(DialogueOption option)
	{
		OnOptionSelected(option.DialogueOptionID);
	}
}
