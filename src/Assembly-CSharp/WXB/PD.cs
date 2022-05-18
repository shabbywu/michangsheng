using System;

namespace WXB
{
	// Token: 0x020009AF RID: 2479
	internal struct PD<T> : IDisposable where T : new()
	{
		// Token: 0x06003F2F RID: 16175 RVA: 0x0002D679 File Offset: 0x0002B879
		public PD(Action<T> free)
		{
			this.value = PoolData<T>.Get();
			this.free = free;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x0002D68D File Offset: 0x0002B88D
		public void Dispose()
		{
			this.free(this.value);
			PoolData<T>.Free(this.value);
		}

		// Token: 0x040038BD RID: 14525
		public T value;

		// Token: 0x040038BE RID: 14526
		private Action<T> free;
	}
}
