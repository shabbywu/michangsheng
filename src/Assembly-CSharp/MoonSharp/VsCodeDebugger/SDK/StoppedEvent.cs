using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C8 RID: 4552
	public class StoppedEvent : Event
	{
		// Token: 0x06006F80 RID: 28544 RVA: 0x0004BC43 File Offset: 0x00049E43
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
