using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D98 RID: 3480
	public class Thread
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06006327 RID: 25383 RVA: 0x0027A66B File Offset: 0x0027886B
		// (set) Token: 0x06006328 RID: 25384 RVA: 0x0027A673 File Offset: 0x00278873
		public int id { get; private set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06006329 RID: 25385 RVA: 0x0027A67C File Offset: 0x0027887C
		// (set) Token: 0x0600632A RID: 25386 RVA: 0x0027A684 File Offset: 0x00278884
		public string name { get; private set; }

		// Token: 0x0600632B RID: 25387 RVA: 0x0027A68D File Offset: 0x0027888D
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
