using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011CB RID: 4555
	public class ThreadEvent : Event
	{
		// Token: 0x06006F83 RID: 28547 RVA: 0x0004BC79 File Offset: 0x00049E79
		public ThreadEvent(string reasn, int tid) : base("thread", new
		{
			reason = reasn,
			threadId = tid
		})
		{
		}
	}
}
