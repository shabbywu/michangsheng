using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001244 RID: 4676
	[CommandInfo("Math", "Negate", "Negate a float", 0)]
	[AddComponentMenu("")]
	public class Neg : BaseUnaryMathCommand
	{
		// Token: 0x060071C7 RID: 29127 RVA: 0x0004D587 File Offset: 0x0004B787
		public override void OnEnter()
		{
			this.outValue.Value = -this.inValue.Value;
			this.Continue();
		}
	}
}
