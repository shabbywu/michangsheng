using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200123A RID: 4666
	[CommandInfo("Math", "Exp", "Command to execute and store the result of a Exp", 0)]
	[AddComponentMenu("")]
	public class Exp : BaseUnaryMathCommand
	{
		// Token: 0x060071AC RID: 29100 RVA: 0x0004D4C0 File Offset: 0x0004B6C0
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Exp(this.inValue.Value);
			this.Continue();
		}
	}
}
