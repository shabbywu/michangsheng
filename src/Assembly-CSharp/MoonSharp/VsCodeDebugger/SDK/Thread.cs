using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C4 RID: 4548
	public class Thread
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06006F6D RID: 28525 RVA: 0x0004BB31 File Offset: 0x00049D31
		// (set) Token: 0x06006F6E RID: 28526 RVA: 0x0004BB39 File Offset: 0x00049D39
		public int id { get; private set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06006F6F RID: 28527 RVA: 0x0004BB42 File Offset: 0x00049D42
		// (set) Token: 0x06006F70 RID: 28528 RVA: 0x0004BB4A File Offset: 0x00049D4A
		public string name { get; private set; }

		// Token: 0x06006F71 RID: 28529 RVA: 0x0004BB53 File Offset: 0x00049D53
		public Thread(int id, string name)
		{
			this.id = id;
			if (name == null || name.Length == 0)
			{
				this.name = string.Format("Thread #{0}", id);
				return;
			}
			this.name = name;
		}
	}
}
