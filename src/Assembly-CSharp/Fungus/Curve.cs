using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF3 RID: 3571
	[CommandInfo("Math", "Curve", "Pass a value through an AnimationCurve", 0)]
	[AddComponentMenu("")]
	public class Curve : BaseUnaryMathCommand
	{
		// Token: 0x0600651C RID: 25884 RVA: 0x00281E41 File Offset: 0x00280041
		public override void OnEnter()
		{
			this.outValue.Value = this.curve.Evaluate(this.inValue.Value);
			this.Continue();
		}

		// Token: 0x040056F1 RID: 22257
		[SerializeField]
		protected AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
	}
}
