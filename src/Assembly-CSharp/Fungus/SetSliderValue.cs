using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02000E3F RID: 3647
	[CommandInfo("UI", "Set Slider Value", "Sets the value property of a slider object", 0)]
	public class SetSliderValue : Command
	{
		// Token: 0x060066A2 RID: 26274 RVA: 0x00286BFA File Offset: 0x00284DFA
		public override void OnEnter()
		{
			if (this.slider != null)
			{
				this.slider.value = this.value;
			}
			this.Continue();
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x00286C26 File Offset: 0x00284E26
		public override string GetSummary()
		{
			if (this.slider == null)
			{
				return "Error: Slider object not selected";
			}
			return this.slider.name + " = " + this.value.GetDescription();
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x00286C5C File Offset: 0x00284E5C
		public override bool HasReference(Variable variable)
		{
			return this.value.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x040057E3 RID: 22499
		[Tooltip("Target slider object to set the value on")]
		[SerializeField]
		protected Slider slider;

		// Token: 0x040057E4 RID: 22500
		[Tooltip("Float value to set the slider value to.")]
		[SerializeField]
		protected FloatData value;
	}
}
