using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001239 RID: 4665
	[CommandInfo("Math", "Curve", "Pass a value through an AnimationCurve", 0)]
	[AddComponentMenu("")]
	public class Curve : BaseUnaryMathCommand
	{
		// Token: 0x060071AA RID: 29098 RVA: 0x0004D470 File Offset: 0x0004B670
		public override void OnEnter()
		{
			this.outValue.Value = this.curve.Evaluate(this.inValue.Value);
			this.Continue();
		}

		// Token: 0x04006408 RID: 25608
		[SerializeField]
		protected AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
	}
}
