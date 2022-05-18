using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D2 RID: 4562
	public class ThreadsResponseBody : ResponseBody
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06006F92 RID: 28562 RVA: 0x0004BD68 File Offset: 0x00049F68
		// (set) Token: 0x06006F93 RID: 28563 RVA: 0x0004BD70 File Offset: 0x00049F70
		public Thread[] threads { get; private set; }

		// Token: 0x06006F94 RID: 28564 RVA: 0x0004BD79 File Offset: 0x00049F79
		public ThreadsResponseBody(List<Thread> vars = null)
		{
			if (vars == null)
			{
				this.threads = new Thread[0];
				return;
			}
			this.threads = vars.ToArray<Thread>();
		}
	}
}
