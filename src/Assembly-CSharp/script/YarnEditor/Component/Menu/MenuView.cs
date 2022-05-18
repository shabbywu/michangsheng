using System;
using Fungus;
using Yarn.Unity;

namespace script.YarnEditor.Component.Menu
{
	// Token: 0x02000AB2 RID: 2738
	public class MenuView : DialogueViewBase
	{
		// Token: 0x0600461D RID: 17949 RVA: 0x001DEDB0 File Offset: 0x001DCFB0
		public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
		{
			MenuDialog menuDialog = MenuDialog.GetMenuDialog();
			for (int i = 0; i < dialogueOptions.Length; i++)
			{
				DialogueOption option = dialogueOptions[i];
				if (option.IsAvailable || this.showUnavailableOptions)
				{
					menuDialog.SetActive(true);
					menuDialog.AddOption(option.Line.Text.Text, true, false, null);
					menuDialog.CachedButtons[menuDialog.NowOption].onClick.RemoveAllListeners();
					menuDialog.CachedButtons[menuDialog.NowOption].onClick.AddListener(delegate()
					{
						this.OptionViewWasSelected(option);
						menuDialog.Clear();
						menuDialog.SetActive(false);
					});
				}
			}
			this.OnOptionSelected = onOptionSelected;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x00032274 File Offset: 0x00030474
		private void OptionViewWasSelected(DialogueOption option)
		{
			this.OnOptionSelected(option.DialogueOptionID);
		}

		// Token: 0x04003E4E RID: 15950
		public Action<int> OnOptionSelected;

		// Token: 0x04003E4F RID: 15951
		private bool showUnavailableOptions;
	}
}
