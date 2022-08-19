using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF5 RID: 3573
	[CommandInfo("Math", "Inverse", "Multiplicative Inverse of a float (1/f)", 0)]
	[AddComponentMenu("")]
	public class Inv : BaseUnaryMathCommand
	{
		// Token: 0x06006520 RID: 25888 RVA: 0x00281EB4 File Offset: 0x002800B4
		public override void OnEnter()
		{
			float value = this.inValue.Value;
			this.outValue.Value = ((value != 0f) ? (1f / this.inValue.Value) : 0f);
			this.Continue();
		}
	}
}
