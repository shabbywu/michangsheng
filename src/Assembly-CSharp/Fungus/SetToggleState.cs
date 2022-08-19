using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E43 RID: 3651
	[CommandInfo("UI", "Set Toggle State", "Sets the state of a toggle UI object", 0)]
	public class SetToggleState : Command
	{
		// Token: 0x060066BE RID: 26302 RVA: 0x00286FC1 File Offset: 0x002851C1
		public override void OnEnter()
		{
			if (this.toggle != null)
			{
				this.toggle.isOn = this.value.Value;
			}
			this.Continue();
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x00286FED File Offset: 0x002851ED
		public override string GetSummary()
		{
			if (this.toggle == null)
			{
				return "Error: Toggle object not selected";
			}
			return this.toggle.name + " = " + this.value.GetDescription();
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x00287023 File Offset: 0x00285223
		public override bool HasReference(Variable variable)
		{
			return this.value.booleanRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057ED RID: 22509
		[Tooltip("Target toggle object to set the state on")]
		[SerializeField]
		protected Toggle toggle;

		// Token: 0x040057EE RID: 22510
		[Tooltip("Boolean value to set the toggle state to.")]
		[SerializeField]
		protected BooleanData value;
	}
}
