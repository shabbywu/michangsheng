using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E3E RID: 3646
	[CommandInfo("Narrative", "Set Say Dialog", "Sets a custom say dialog to use when displaying story text", 0)]
	[AddComponentMenu("")]
	public class SetSayDialog : Command
	{
		// Token: 0x0600669E RID: 26270 RVA: 0x00286BB8 File Offset: 0x00284DB8
		public override void OnEnter()
		{
			if (this.sayDialog != null)
			{
				SayDialog.ActiveSayDialog = this.sayDialog;
			}
			this.Continue();
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x00286BD9 File Offset: 0x00284DD9
		public override string GetSummary()
		{
			if (this.sayDialog == null)
			{
				return "Error: No say dialog selected";
			}
			return this.sayDialog.name;
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x040057E2 RID: 22498
		[Tooltip("The Say Dialog to use for displaying Say story text")]
		[SerializeField]
		protected SayDialog sayDialog;
	}
}
