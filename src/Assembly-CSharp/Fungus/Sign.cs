using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001248 RID: 4680
	[CommandInfo("Math", "Sign", "Command to execute and store the result of a Sign", 0)]
	[AddComponentMenu("")]
	public class Sign : BaseUnaryMathCommand
	{
		// Token: 0x060071D1 RID: 29137 RVA: 0x0004D632 File Offset: 0x0004B832
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Sign(this.inValue.Value);
			this.Continue();
		}
	}
}
