using System;
using System.IO;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C5 RID: 4549
	public class Source
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06006F72 RID: 28530 RVA: 0x0004BB8B File Offset: 0x00049D8B
		// (set) Token: 0x06006F73 RID: 28531 RVA: 0x0004BB93 File Offset: 0x00049D93
		public string name { get; private set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06006F74 RID: 28532 RVA: 0x0004BB9C File Offset: 0x00049D9C
		// (set) Token: 0x06006F75 RID: 28533 RVA: 0x0004BBA4 File Offset: 0x00049DA4
		public string path { get; private set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06006F76 RID: 28534 RVA: 0x0004BBAD File Offset: 0x00049DAD
		// (set) Token: 0x06006F77 RID: 28535 RVA: 0x0004BBB5 File Offset: 0x00049DB5
		public int sourceReference { get; private set; }

		// Token: 0x06006F78 RID: 28536 RVA: 0x0004BBBE File Offset: 0x00049DBE
		public Source(string name, string path, int sourceReference = 0)
		{
			this.name = name;
			this.path = path;
			this.sourceReference = sourceReference;
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x0004BBDB File Offset: 0x00049DDB
		public Source(string path, int sourceReference = 0)
		{
			this.name = Path.GetFileName(path);
			this.path = path;
			this.sourceReference = sourceReference;
		}
	}
}
