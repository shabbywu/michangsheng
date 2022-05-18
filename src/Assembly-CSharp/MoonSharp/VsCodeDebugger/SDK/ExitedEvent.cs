using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C9 RID: 4553
	public class ExitedEvent : Event
	{
		// Token: 0x06006F81 RID: 28545 RVA: 0x0004BC58 File Offset: 0x00049E58
		public ExitedEvent(int exCode) : base("exited", new
		{
			exitCode = exCode
		})
		{
		}
	}
}
