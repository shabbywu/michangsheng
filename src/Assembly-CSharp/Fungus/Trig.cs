using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200124C RID: 4684
	[CommandInfo("Math", "Trig", "Command to execute and store the result of basic trigonometry", 0)]
	[AddComponentMenu("")]
	public class Trig : BaseUnaryMathCommand
	{
		// Token: 0x060071DA RID: 29146 RVA: 0x002A6DDC File Offset: 0x002A4FDC
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

		// Token: 0x060071DB RID: 29147 RVA: 0x0004D6A0 File Offset: 0x0004B8A0
		public override string GetSummary()
		{
			return this.function.ToString() + " " + base.GetSummary();
		}

		// Token: 0x04006437 RID: 25655
		[Tooltip("Trigonometric function to run.")]
		[SerializeField]
		protected Trig.Function function = Trig.Function.Sin;

		// Token: 0x0200124D RID: 4685
		public enum Function
		{
			// Token: 0x04006439 RID: 25657
			Rad2Deg,
			// Token: 0x0400643A RID: 25658
			Deg2Rad,
			// Token: 0x0400643B RID: 25659
			ACos,
			// Token: 0x0400643C RID: 25660
			ASin,
			// Token: 0x0400643D RID: 25661
			ATan,
			// Token: 0x0400643E RID: 25662
			Cos,
			// Token: 0x0400643F RID: 25663
			Sin,
			// Token: 0x04006440 RID: 25664
			Tan
		}
	}
}
