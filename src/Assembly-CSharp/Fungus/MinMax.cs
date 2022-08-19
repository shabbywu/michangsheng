using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFA RID: 3578
	[CommandInfo("Math", "MinMax", "Command to store the min or max of 2 values", 0)]
	[AddComponentMenu("")]
	public class MinMax : Command
	{
		// Token: 0x06006534 RID: 25908 RVA: 0x002824AC File Offset: 0x002806AC
		public override void OnEnter()
		{
			MinMax.Function function = this.function;
			if (function != MinMax.Function.Min)
			{
				if (function == MinMax.Function.Max)
				{
					this.outValue.Value = Mathf.Max(this.inLHSValue.Value, this.inRHSValue.Value);
				}
			}
			else
			{
				this.outValue.Value = Mathf.Min(this.inLHSValue.Value, this.inRHSValue.Value);
			}
			this.Continue();
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x00282520 File Offset: 0x00280720
		public override string GetSummary()
		{
			return string.Concat(new string[]
			{
				this.function.ToString(),
				" out: ",
				(this.outValue.floatRef != null) ? this.outValue.floatRef.Key : this.outValue.Value.ToString(),
				" [",
				this.inLHSValue.Value.ToString(),
				" - ",
				this.inRHSValue.Value.ToString(),
				"]"
			});
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x002825D3 File Offset: 0x002807D3
		public override bool HasReference(Variable variable)
		{
			return this.inLHSValue.floatRef == variable || this.inRHSValue.floatRef == variable || this.outValue.floatRef == variable;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x0027D3DB File Offset: 0x0027B5DB
		public override Color GetButtonColor()
		{
			return new Color32(235, 191, 217, byte.MaxValue);
		}

		// Token: 0x04005703 RID: 22275
		[Tooltip("Min Or Max")]
		[SerializeField]
		protected MinMax.Function function;

		// Token: 0x04005704 RID: 22276
		[SerializeField]
		protected FloatData inLHSValue;

		// Token: 0x04005705 RID: 22277
		[SerializeField]
		protected FloatData inRHSValue;

		// Token: 0x04005706 RID: 22278
		[SerializeField]
		protected FloatData outValue;

		// Token: 0x020016BC RID: 5820
		public enum Function
		{
			// Token: 0x0400737A RID: 29562
			Min,
			// Token: 0x0400737B RID: 29563
			Max
		}
	}
}
