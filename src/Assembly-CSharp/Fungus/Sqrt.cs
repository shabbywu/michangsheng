using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFF RID: 3583
	[CommandInfo("Math", "Sqrt", "Command to execute and store the result of a Sqrt", 0)]
	[AddComponentMenu("")]
	public class Sqrt : BaseUnaryMathCommand
	{
		// Token: 0x06006545 RID: 25925 RVA: 0x002827E1 File Offset: 0x002809E1
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Sqrt(this.inValue.Value);
			this.Continue();
		}
	}
}
