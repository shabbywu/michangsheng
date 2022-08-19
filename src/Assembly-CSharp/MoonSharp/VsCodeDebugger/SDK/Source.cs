using System;
using System.IO;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D99 RID: 3481
	public class Source
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600632C RID: 25388 RVA: 0x0027A6C5 File Offset: 0x002788C5
		// (set) Token: 0x0600632D RID: 25389 RVA: 0x0027A6CD File Offset: 0x002788CD
		public string name { get; private set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600632E RID: 25390 RVA: 0x0027A6D6 File Offset: 0x002788D6
		// (set) Token: 0x0600632F RID: 25391 RVA: 0x0027A6DE File Offset: 0x002788DE
		public string path { get; private set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06006330 RID: 25392 RVA: 0x0027A6E7 File Offset: 0x002788E7
		// (set) Token: 0x06006331 RID: 25393 RVA: 0x0027A6EF File Offset: 0x002788EF
		public int sourceReference { get; private set; }

		// Token: 0x06006332 RID: 25394 RVA: 0x0027A6F8 File Offset: 0x002788F8
		public Source(string name, string path, int sourceReference = 0)
		{
			this.name = name;
			this.path = path;
			this.sourceReference = sourceReference;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0027A715 File Offset: 0x00278915
		public Source(string path, int sourceReference = 0)
		{
			this.name = Path.GetFileName(path);
			this.path = path;
			this.sourceReference = sourceReference;
		}
	}
}
