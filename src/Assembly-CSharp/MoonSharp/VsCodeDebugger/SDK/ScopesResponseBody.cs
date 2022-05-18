using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D0 RID: 4560
	public class ScopesResponseBody : ResponseBody
	{
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06006F8C RID: 28556 RVA: 0x0004BCFE File Offset: 0x00049EFE
		// (set) Token: 0x06006F8D RID: 28557 RVA: 0x0004BD06 File Offset: 0x00049F06
		public Scope[] scopes { get; private set; }

		// Token: 0x06006F8E RID: 28558 RVA: 0x0004BD0F File Offset: 0x00049F0F
		public ScopesResponseBody(List<Scope> scps = null)
		{
			if (scps == null)
			{
				this.scopes = new Scope[0];
				return;
			}
			this.scopes = scps.ToArray<Scope>();
		}
	}
}
