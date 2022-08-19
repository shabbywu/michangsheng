using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA4 RID: 3492
	public class ScopesResponseBody : ResponseBody
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x0027A838 File Offset: 0x00278A38
		// (set) Token: 0x06006347 RID: 25415 RVA: 0x0027A840 File Offset: 0x00278A40
		public Scope[] scopes { get; private set; }

		// Token: 0x06006348 RID: 25416 RVA: 0x0027A849 File Offset: 0x00278A49
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
