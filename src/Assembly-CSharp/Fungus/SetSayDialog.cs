using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200128F RID: 4751
	[CommandInfo("Narrative", "Set Say Dialog", "Sets a custom say dialog to use when displaying story text", 0)]
	[AddComponentMenu("")]
	public class SetSayDialog : Command
	{
		// Token: 0x0600732C RID: 29484 RVA: 0x0004E893 File Offset: 0x0004CA93
		public override void OnEnter()
		{
			if (this.sayDialog != null)
			{
				SayDialog.ActiveSayDialog = this.sayDialog;
			}
			this.Continue();
		}

		// Token: 0x0600732D RID: 29485 RVA: 0x0004E8B4 File Offset: 0x0004CAB4
		public override string GetSummary()
		{
			if (this.sayDialog == null)
			{
				return "Error: No say dialog selected";
			}
			return this.sayDialog.name;
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006526 RID: 25894
		[Tooltip("The Say Dialog to use for displaying Say story text")]
		[SerializeField]
		protected SayDialog sayDialog;
	}
}
