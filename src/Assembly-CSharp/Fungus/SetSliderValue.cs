using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fungus
{
	// Token: 0x02001290 RID: 4752
	[CommandInfo("UI", "Set Slider Value", "Sets the value property of a slider object", 0)]
	public class SetSliderValue : Command
	{
		// Token: 0x06007330 RID: 29488 RVA: 0x0004E8D5 File Offset: 0x0004CAD5
		public override void OnEnter()
		{
			if (this.slider != null)
			{
				this.slider.value = this.value;
			}
			this.Continue();
		}

		// Token: 0x06007331 RID: 29489 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x0004E901 File Offset: 0x0004CB01
		public override string GetSummary()
		{
			if (this.slider == null)
			{
				return "Error: Slider object not selected";
			}
			return this.slider.name + " = " + this.value.GetDescription();
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x0004E937 File Offset: 0x0004CB37
		public override bool HasReference(Variable variable)
		{
			return this.value.floatRef == variable || base.HasReference(variable);
		}

		// Token: 0x04006527 RID: 25895
		[Tooltip("Target slider object to set the value on")]
		[SerializeField]
		protected Slider slider;

		// Token: 0x04006528 RID: 25896
		[Tooltip("Float value to set the slider value to.")]
		[SerializeField]
		protected FloatData value;
	}
}
