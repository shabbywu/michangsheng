using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001237 RID: 4663
	[CommandInfo("Math", "Clamp", "Command to contain a value between a lower and upper bound, with optional wrapping modes", 0)]
	[AddComponentMenu("")]
	public class Clamp : Command
	{
		// Token: 0x060071A5 RID: 29093 RVA: 0x002A6358 File Offset: 0x002A4558
		public override void OnEnter()
		{
			float num = this.lower.Value;
			float num2 = this.upper.Value;
			float num3 = this.value.Value;
			switch (this.mode)
			{
			case Clamp.Mode.Clamp:
				this.outValue.Value = Mathf.Clamp(this.value.Value, this.lower.Value, this.upper.Value);
				break;
			case Clamp.Mode.Repeat:
				this.outValue.Value = Mathf.Repeat(num3 - num, num2 - num) + num;
				break;
			case Clamp.Mode.PingPong:
				this.outValue.Value = Mathf.PingPong(num3 - num, num2 - num) + num;
				break;
			}
			this.Continue();
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x002A6410 File Offset: 0x002A4610
		public override string GetSummary()
		{
			if (this.outValue.floatRef == null)
			{
				return "Error: no output value selected";
			}
			return this.outValue.floatRef.Key + " = " + Clamp.Mode.Clamp.ToString() + ((this.mode != Clamp.Mode.Clamp) ? (" & " + this.mode.ToString()) : "");
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x002A648C File Offset: 0x002A468C
		public override bool HasReference(Variable variable)
		{
			return this.lower.floatRef == variable || this.upper.floatRef == variable || this.value.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x0004C5E0 File Offset: 0x0004A7E0
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040063FF RID: 25599
		[SerializeField]
		protected Clamp.Mode mode;

		// Token: 0x04006400 RID: 25600
		[SerializeField]
		protected FloatData lower;

		// Token: 0x04006401 RID: 25601
		[SerializeField]
		protected FloatData upper;

		// Token: 0x04006402 RID: 25602
		[SerializeField]
		protected FloatData value;

		// Token: 0x04006403 RID: 25603
		[Tooltip("Result put here, if using pingpong don't use the same var for value as outValue.")]
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x02001238 RID: 4664
		public enum Mode
		{
			// Token: 0x04006405 RID: 25605
			Clamp,
			// Token: 0x04006406 RID: 25606
			Repeat,
			// Token: 0x04006407 RID: 25607
			PingPong
		}
	}
}
