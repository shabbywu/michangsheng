using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E3B RID: 3643
	[CommandInfo("Narrative", "Set Menu Dialog", "Sets a custom menu dialog to use when displaying multiple choice menus", 0)]
	[AddComponentMenu("")]
	public class SetMenuDialog : Command
	{
		// Token: 0x0600668F RID: 26255 RVA: 0x00286AD2 File Offset: 0x00284CD2
		public override void OnEnter()
		{
			if (this.menuDialog != null)
			{
				MenuDialog.ActiveMenuDialog = this.menuDialog;
			}
			this.Continue();
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x00286AF3 File Offset: 0x00284CF3
		public override string GetSummary()
		{
			if (this.menuDialog == null)
			{
				return "Error: No menu dialog selected";
			}
			return this.menuDialog.name;
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x040057DB RID: 22491
		[Tooltip("The Menu Dialog to use for displaying menu buttons")]
		[SerializeField]
		protected MenuDialog menuDialog;
	}
}
