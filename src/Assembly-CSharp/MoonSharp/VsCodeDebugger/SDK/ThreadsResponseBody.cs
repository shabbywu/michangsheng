using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA6 RID: 3494
	public class ThreadsResponseBody : ResponseBody
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600634C RID: 25420 RVA: 0x0027A8A2 File Offset: 0x00278AA2
		// (set) Token: 0x0600634D RID: 25421 RVA: 0x0027A8AA File Offset: 0x00278AAA
		public Thread[] threads { get; private set; }

		// Token: 0x0600634E RID: 25422 RVA: 0x0027A8B3 File Offset: 0x00278AB3
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
