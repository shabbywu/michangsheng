using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E35 RID: 3637
	[CommandInfo("Sprite", "Set Clickable 2D", "Sets a Clickable2D component to be clickable / non-clickable.", 0)]
	[AddComponentMenu("")]
	public class SetClickable2D : Command
	{
		// Token: 0x0600666B RID: 26219 RVA: 0x002864F9 File Offset: 0x002846F9
		public override void OnEnter()
		{
			if (this.targetClickable2D != null)
			{
				this.targetClickable2D.ClickEnabled = this.activeState.Value;
			}
			this.Continue();
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x00286525 File Offset: 0x00284725
		public override string GetSummary()
		{
			if (this.targetClickable2D == null)
			{
				return "Error: No Clickable2D component selected";
			}
			return this.targetClickable2D.gameObject.name;
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x0028654B File Offset: 0x0028474B
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057CD RID: 22477
		[Tooltip("Reference to Clickable2D component on a gameobject")]
		[SerializeField]
		protected Clickable2D targetClickable2D;

		// Token: 0x040057CE RID: 22478
		[Tooltip("Set to true to enable the component")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
