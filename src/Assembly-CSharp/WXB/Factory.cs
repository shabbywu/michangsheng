using System;

namespace WXB
{
	// Token: 0x0200069D RID: 1693
	internal class Factory<T> : IFactory where T : new()
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x00170C6E File Offset: 0x0016EE6E
		public Factory(Action<T> f)
		{
			this.free = f;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00170C7D File Offset: 0x0016EE7D
		public object create()
		{
			return new PD<T>(this.free);
		}

		// Token: 0x04002F06 RID: 12038
		public Action<T> free;
	}
}
