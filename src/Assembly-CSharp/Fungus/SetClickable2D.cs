using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001286 RID: 4742
	[CommandInfo("Sprite", "Set Clickable 2D", "Sets a Clickable2D component to be clickable / non-clickable.", 0)]
	[AddComponentMenu("")]
	public class SetClickable2D : Command
	{
		// Token: 0x060072F9 RID: 29433 RVA: 0x0004E53F File Offset: 0x0004C73F
		public override void OnEnter()
		{
			if (this.targetClickable2D != null)
			{
				this.targetClickable2D.ClickEnabled = this.activeState.Value;
			}
			this.Continue();
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x0004E56B File Offset: 0x0004C76B
		public override string GetSummary()
		{
			if (this.targetClickable2D == null)
			{
				return "Error: No Clickable2D component selected";
			}
			return this.targetClickable2D.gameObject.name;
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x0004E591 File Offset: 0x0004C791
		public override bool HasReference(Variable variable)
		{
			return this.activeState.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006511 RID: 25873
		[Tooltip("Reference to Clickable2D component on a gameobject")]
		[SerializeField]
		protected Clickable2D targetClickable2D;

		// Token: 0x04006512 RID: 25874
		[Tooltip("Set to true to enable the component")]
		[SerializeField]
		protected BooleanData activeState;
	}
}
