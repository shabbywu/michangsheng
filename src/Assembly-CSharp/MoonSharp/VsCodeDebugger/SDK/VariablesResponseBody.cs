using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D1 RID: 4561
	public class VariablesResponseBody : ResponseBody
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06006F8F RID: 28559 RVA: 0x0004BD33 File Offset: 0x00049F33
		// (set) Token: 0x06006F90 RID: 28560 RVA: 0x0004BD3B File Offset: 0x00049F3B
		public Variable[] variables { get; private set; }

		// Token: 0x06006F91 RID: 28561 RVA: 0x0004BD44 File Offset: 0x00049F44
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
