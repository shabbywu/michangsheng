using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001294 RID: 4756
	[CommandInfo("UI", "Set Toggle State", "Sets the state of a toggle UI object", 0)]
	public class SetToggleState : Command
	{
		// Token: 0x0600734C RID: 29516 RVA: 0x0004EAA5 File Offset: 0x0004CCA5
		public override void OnEnter()
		{
			if (this.toggle != null)
			{
				this.toggle.isOn = this.value.Value;
			}
			this.Continue();
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x0004EAD1 File Offset: 0x0004CCD1
		public override string GetSummary()
		{
			if (this.toggle == null)
			{
				return "Error: Toggle object not selected";
			}
			return this.toggle.name + " = " + this.value.GetDescription();
		}

		// Token: 0x0600734F RID: 29519 RVA: 0x0004EB07 File Offset: 0x0004CD07
		public override bool HasReference(Variable variable)
		{
			return this.value.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006531 RID: 25905
		[Tooltip("Target toggle object to set the state on")]
		[SerializeField]
		protected Toggle toggle;

		// Token: 0x04006532 RID: 25906
		[Tooltip("Boolean value to set the toggle state to.")]
		[SerializeField]
		protected BooleanData value;
	}
}
