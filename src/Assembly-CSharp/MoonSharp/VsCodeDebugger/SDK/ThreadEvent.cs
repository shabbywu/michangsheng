using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D9F RID: 3487
	public class ThreadEvent : Event
	{
		// Token: 0x0600633D RID: 25405 RVA: 0x0027A7B3 File Offset: 0x002789B3
		public ThreadEvent(string reasn, int tid) : base("thread", new
		{
			reason = reasn,
			threadId = tid
		})
		{
		}
	}
}
