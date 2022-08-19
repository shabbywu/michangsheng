using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000DF0 RID: 3568
	[CommandInfo("Math", "Abs", "Command to execute and store the result of a Abs", 0)]
	[AddComponentMenu("")]
	public class Abs : BaseUnaryMathCommand
	{
		// Token: 0x06006511 RID: 25873 RVA: 0x00281BD5 File Offset: 0x0027FDD5
		public override void OnEnter()
		{
			this.outValue.Value = Mathf.Abs(this.inValue.Value);
			this.Continue();
		}
	}
}
