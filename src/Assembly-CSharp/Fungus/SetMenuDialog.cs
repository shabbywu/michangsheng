using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200128C RID: 4748
	[CommandInfo("Narrative", "Set Menu Dialog", "Sets a custom menu dialog to use when displaying multiple choice menus", 0)]
	[AddComponentMenu("")]
	public class SetMenuDialog : Command
	{
		// Token: 0x0600731D RID: 29469 RVA: 0x0004E7AD File Offset: 0x0004C9AD
		public override void OnEnter()
		{
			if (this.menuDialog != null)
			{
				MenuDialog.ActiveMenuDialog = this.menuDialog;
			}
			this.Continue();
		}

		// Token: 0x0600731E RID: 29470 RVA: 0x0004E7CE File Offset: 0x0004C9CE
		public override string GetSummary()
		{
			if (this.menuDialog == null)
			{
				return "Error: No menu dialog selected";
			}
			return this.menuDialog.name;
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x0400651F RID: 25887
		[Tooltip("The Menu Dialog to use for displaying menu buttons")]
		[SerializeField]
		protected MenuDialog menuDialog;
	}
}
