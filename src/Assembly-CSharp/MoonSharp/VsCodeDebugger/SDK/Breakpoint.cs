using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D9A RID: 3482
	public class Breakpoint
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06006334 RID: 25396 RVA: 0x0027A737 File Offset: 0x00278937
		// (set) Token: 0x06006335 RID: 25397 RVA: 0x0027A73F File Offset: 0x0027893F
		public bool verified { get; private set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06006336 RID: 25398 RVA: 0x0027A748 File Offset: 0x00278948
		// (set) Token: 0x06006337 RID: 25399 RVA: 0x0027A750 File Offset: 0x00278950
		public int line { get; private set; }

		// Token: 0x06006338 RID: 25400 RVA: 0x0027A759 File Offset: 0x00278959
		public Breakpoint(bool verified, int line)
		{
			this.verified = verified;
			this.line = line;
		}
	}
}
