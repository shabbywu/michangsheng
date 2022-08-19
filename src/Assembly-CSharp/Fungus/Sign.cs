using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DFE RID: 3582
	[CommandInfo("Math", "Sign", "Command to execute and store the result of a Sign", 0)]
	[AddComponentMenu("")]
	public class Sign : BaseUnaryMathCommand
	{
		// Token: 0x06006543 RID: 25923 RVA: 0x002827BE File Offset: 0x002809BE
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Sign(this.inValue.Value);
			this.Continue();
		}
	}
}
