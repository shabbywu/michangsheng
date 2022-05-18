using System;

namespace WXB
{
	// Token: 0x020009B1 RID: 2481
	internal class Factory<T> : IFactory where T : new()
	{
		// Token: 0x06003F32 RID: 16178 RVA: 0x0002D6AB File Offset: 0x0002B8AB
		public Factory(Action<T> f)
		{
			this.free = f;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0002D6BA File Offset: 0x0002B8BA
		public object create()
		{
			return new PD<T>(this.free);
		}

		// Token: 0x040038BF RID: 14527
		public Action<T> free;
	}
}
