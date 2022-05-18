using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C6 RID: 4550
	public class Breakpoint
	{
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06006F7A RID: 28538 RVA: 0x0004BBFD File Offset: 0x00049DFD
		// (set) Token: 0x06006F7B RID: 28539 RVA: 0x0004BC05 File Offset: 0x00049E05
		public bool verified { get; private set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06006F7C RID: 28540 RVA: 0x0004BC0E File Offset: 0x00049E0E
		// (set) Token: 0x06006F7D RID: 28541 RVA: 0x0004BC16 File Offset: 0x00049E16
		public int line { get; private set; }

		// Token: 0x06006F7E RID: 28542 RVA: 0x0004BC1F File Offset: 0x00049E1F
		public Breakpoint(bool verified, int line)
		{
			this.verified = verified;
			this.line = line;
		}
	}
}
