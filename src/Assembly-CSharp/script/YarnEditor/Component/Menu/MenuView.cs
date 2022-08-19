using System;
using Fungus;
using Yarn.Unity;

namespace script.YarnEditor.Component.Menu
{
	// Token: 0x020009CB RID: 2507
	public class MenuView : DialogueViewBase
	{
		// Token: 0x060045C1 RID: 17857 RVA: 0x001D8FDC File Offset: 0x001D71DC
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

		// Token: 0x060045C2 RID: 17858 RVA: 0x001D90E8 File Offset: 0x001D72E8
		private void OptionViewWasSelected(DialogueOption option)
		{
			this.OnOptionSelected(option.DialogueOptionID);
		}

		// Token: 0x0400473D RID: 18237
		public Action<int> OnOptionSelected;

		// Token: 0x0400473E RID: 18238
		private bool showUnavailableOptions;
	}
}
