using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF4 RID: 3572
	[CommandInfo("Math", "Exp", "Command to execute and store the result of a Exp", 0)]
	[AddComponentMenu("")]
	public class Exp : BaseUnaryMathCommand
	{
		// Token: 0x0600651E RID: 25886 RVA: 0x00281E91 File Offset: 0x00280091
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Exp(this.inValue.Value);
			this.Continue();
		}
	}
}
