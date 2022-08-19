using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA2 RID: 3490
	public class ErrorResponseBody : ResponseBody
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06006340 RID: 25408 RVA: 0x0027A7E3 File Offset: 0x002789E3
		// (set) Token: 0x06006341 RID: 25409 RVA: 0x0027A7EB File Offset: 0x002789EB
		public Message error { get; private set; }

		// Token: 0x06006342 RID: 25410 RVA: 0x0027A7F4 File Offset: 0x002789F4
		public ErrorResponseBody(Message error)
		{
			this.error = error;
		}
	}
}
