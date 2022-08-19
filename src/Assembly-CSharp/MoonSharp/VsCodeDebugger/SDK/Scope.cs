using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D96 RID: 3478
	public class Scope
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06006319 RID: 25369 RVA: 0x0027A5CB File Offset: 0x002787CB
		// (set) Token: 0x0600631A RID: 25370 RVA: 0x0027A5D3 File Offset: 0x002787D3
		public string name { get; private set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600631B RID: 25371 RVA: 0x0027A5DC File Offset: 0x002787DC
		// (set) Token: 0x0600631C RID: 25372 RVA: 0x0027A5E4 File Offset: 0x002787E4
		public int variablesReference { get; private set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600631D RID: 25373 RVA: 0x0027A5ED File Offset: 0x002787ED
		// (set) Token: 0x0600631E RID: 25374 RVA: 0x0027A5F5 File Offset: 0x002787F5
		public bool expensive { get; private set; }

		// Token: 0x0600631F RID: 25375 RVA: 0x0027A5FE File Offset: 0x002787FE
		public Scope(string name, int variablesReference, bool expensive = false)
		{
			this.name = name;
			this.variablesReference = variablesReference;
			this.expensive = expensive;
		}
	}
}
