using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFB RID: 3579
	[CommandInfo("Math", "Negate", "Negate a float", 0)]
	[AddComponentMenu("")]
	public class Neg : BaseUnaryMathCommand
	{
		// Token: 0x06006539 RID: 25913 RVA: 0x0028260E File Offset: 0x0028080E
		public override void OnEnter()
		{
			this.outValue.Value = -this.inValue.Value;
			this.Continue();
		}
	}
}
