using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D9D RID: 3485
	public class ExitedEvent : Event
	{
		// Token: 0x0600633B RID: 25403 RVA: 0x0027A792 File Offset: 0x00278992
		public ExitedEvent(int exCode) : base("exited", new
		{
			exitCode = exCode
		})
		{
		}
	}
}
