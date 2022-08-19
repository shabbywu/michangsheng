using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA5 RID: 3493
	public class VariablesResponseBody : ResponseBody
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06006349 RID: 25417 RVA: 0x0027A86D File Offset: 0x00278A6D
		// (set) Token: 0x0600634A RID: 25418 RVA: 0x0027A875 File Offset: 0x00278A75
		public Variable[] variables { get; private set; }

		// Token: 0x0600634B RID: 25419 RVA: 0x0027A87E File Offset: 0x00278A7E
		public VariablesResponseBody(List<Variable> vars = null)
		{
			if (vars == null)
			{
				this.variables = new Variable[0];
				return;
			}
			this.variables = vars.ToArray<Variable>();
		}
	}
}
