using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000E01 RID: 3585
	[CommandInfo("Math", "Trig", "Command to execute and store the result of basic trigonometry", 0)]
	[AddComponentMenu("")]
	public class Trig : BaseUnaryMathCommand
	{
		// Token: 0x0600654C RID: 25932 RVA: 0x00282964 File Offset: 0x00280B64
		public override void OnEnter()
		{
			switch (this.function)
			{
			case Trig.Function.Rad2Deg:
				this.outValue.Value = this.inValue.Value * 57.29578f;
				break;
			case Trig.Function.Deg2Rad:
				this.outValue.Value = this.inValue.Value * 0.017453292f;
				break;
			case Trig.Function.ACos:
				this.outValue.Value = Mathf.Acos(this.inValue.Value);
				break;
			case Trig.Function.ASin:
				this.outValue.Value = Mathf.Asin(this.inValue.Value);
				break;
			case Trig.Function.ATan:
				this.outValue.Value = Mathf.Atan(this.inValue.Value);
				break;
			case Trig.Function.Cos:
				this.outValue.Value = Mathf.Cos(this.inValue.Value);
				break;
			case Trig.Function.Sin:
				this.outValue.Value = Mathf.Sin(this.inValue.Value);
				break;
			case Trig.Function.Tan:
				this.outValue.Value = Mathf.Tan(this.inValue.Value);
				break;
			}
			this.Continue();
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x00282A9A File Offset: 0x00280C9A
		public override string GetSummary()
		{
			return this.function.ToString() + " " + base.GetSummary();
		}

		// Token: 0x0400570E RID: 22286
		[Tooltip("Trigonometric function to run.")]
		[SerializeField]
		protected Trig.Function function = Trig.Function.Sin;

		// Token: 0x020016BF RID: 5823
		public enum Function
		{
			// Token: 0x04007385 RID: 29573
			Rad2Deg,
			// Token: 0x04007386 RID: 29574
			Deg2Rad,
			// Token: 0x04007387 RID: 29575
			ACos,
			// Token: 0x04007388 RID: 29576
			ASin,
			// Token: 0x04007389 RID: 29577
			ATan,
			// Token: 0x0400738A RID: 29578
			Cos,
			// Token: 0x0400738B RID: 29579
			Sin,
			// Token: 0x0400738C RID: 29580
			Tan
		}
	}
}
