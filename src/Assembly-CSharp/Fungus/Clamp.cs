using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF2 RID: 3570
	[CommandInfo("Math", "Clamp", "Command to contain a value between a lower and upper bound, with optional wrapping modes", 0)]
	[AddComponentMenu("")]
	public class Clamp : Command
	{
		// Token: 0x06006517 RID: 25879 RVA: 0x00281CB4 File Offset: 0x0027FEB4
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

		// Token: 0x06006518 RID: 25880 RVA: 0x00281D6C File Offset: 0x0027FF6C
		public override string GetSummary()
		{
			if (this.outValue.floatRef == null)
			{
				return "Error: no output value selected";
			}
			return this.outValue.floatRef.Key + " = " + Clamp.Mode.Clamp.ToString() + ((this.mode != Clamp.Mode.Clamp) ? (" & " + this.mode.ToString()) : "");
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00281DE8 File Offset: 0x0027FFE8
		public override bool HasReference(Variable variable)
		{
			return this.lower.floatRef == variable || this.upper.floatRef == variable || this.value.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x040056EC RID: 22252
		[SerializeField]
		protected Clamp.Mode mode;

		// Token: 0x040056ED RID: 22253
		[SerializeField]
		protected FloatData lower;

		// Token: 0x040056EE RID: 22254
		[SerializeField]
		protected FloatData upper;

		// Token: 0x040056EF RID: 22255
		[SerializeField]
		protected FloatData value;

		// Token: 0x040056F0 RID: 22256
		[Tooltip("Result put here, if using pingpong don't use the same var for value as outValue.")]
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x020016B9 RID: 5817
		public enum Mode
		{
			// Token: 0x0400736F RID: 29551
			Clamp,
			// Token: 0x04007370 RID: 29552
			Repeat,
			// Token: 0x04007371 RID: 29553
			PingPong
		}
	}
}
