using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011CE RID: 4558
	public class ErrorResponseBody : ResponseBody
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x0004BCA9 File Offset: 0x00049EA9
		// (set) Token: 0x06006F87 RID: 28551 RVA: 0x0004BCB1 File Offset: 0x00049EB1
		public Message error { get; private set; }

		// Token: 0x06006F88 RID: 28552 RVA: 0x0004BCBA File Offset: 0x00049EBA
		public ErrorResponseBody(Message error)
		{
			this.error = error;
		}
	}
}
