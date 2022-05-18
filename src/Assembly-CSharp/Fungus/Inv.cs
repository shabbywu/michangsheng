using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200123B RID: 4667
	[CommandInfo("Math", "Inverse", "Multiplicative Inverse of a float (1/f)", 0)]
	[AddComponentMenu("")]
	public class Inv : BaseUnaryMathCommand
	{
		// Token: 0x060071AE RID: 29102 RVA: 0x002A64E8 File Offset: 0x002A46E8
		public override void OnEnter()
		{
			float value = this.inValue.Value;
			this.outValue.Value = ((value != 0f) ? (1f / this.inValue.Value) : 0f);
			this.Continue();
		}
	}
}
