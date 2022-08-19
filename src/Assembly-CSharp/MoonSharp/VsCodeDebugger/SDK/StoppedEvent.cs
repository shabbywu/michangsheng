using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D9C RID: 3484
	public class StoppedEvent : Event
	{
		// Token: 0x0600633A RID: 25402 RVA: 0x0027A77D File Offset: 0x0027897D
		public StoppedEvent(int tid, string reasn, string txt = null) : base("stopped", new
		{
			threadId = tid,
			reason = reasn,
			text = txt
		})
		{
		}
	}
}
