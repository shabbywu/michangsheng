using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001235 RID: 4661
	[CommandInfo("Math", "Abs", "Command to execute and store the result of a Abs", 0)]
	[AddComponentMenu("")]
	public class Abs : BaseUnaryMathCommand
	{
		// Token: 0x0600719F RID: 29087 RVA: 0x0004D41D File Offset: 0x0004B61D
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Abs(this.inValue.Value);
			this.Continue();
		}
	}
}
