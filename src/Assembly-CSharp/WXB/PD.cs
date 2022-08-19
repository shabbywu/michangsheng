using System;

namespace WXB
{
	// Token: 0x0200069B RID: 1691
	internal struct PD<T> : IDisposable where T : new()
	{
		// Token: 0x06003571 RID: 13681 RVA: 0x00170C3C File Offset: 0x0016EE3C
		public PD(Action<T> free)
		{
			this.value = PoolData<T>.Get();
			this.free = free;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x00170C50 File Offset: 0x0016EE50
		public void Dispose()
		{
			this.free(this.value);
			PoolData<T>.Free(this.value);
		}

		// Token: 0x04002F04 RID: 12036
		public T value;

		// Token: 0x04002F05 RID: 12037
		private Action<T> free;
	}
}
