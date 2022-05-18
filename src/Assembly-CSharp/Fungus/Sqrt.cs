using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001249 RID: 4681
	[CommandInfo("Math", "Sqrt", "Command to execute and store the result of a Sqrt", 0)]
	[AddComponentMenu("")]
	public class Sqrt : BaseUnaryMathCommand
	{
		// Token: 0x060071D3 RID: 29139 RVA: 0x0004D655 File Offset: 0x0004B855
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Sqrt(this.inValue.Value);
			this.Continue();
		}
	}
}
