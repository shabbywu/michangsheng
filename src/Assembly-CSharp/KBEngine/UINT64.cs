using System;

namespace KBEngine
{
	// Token: 0x02000B9C RID: 2972
	public struct UINT64
	{
		// Token: 0x0600531E RID: 21278 RVA: 0x00233942 File Offset: 0x00231B42
		private UINT64(ulong value)
		{
			this.value = value;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0023394B File Offset: 0x00231B4B
		public static implicit operator ulong(UINT64 value)
		{
			return value.value;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00233953 File Offset: 0x00231B53
		public static implicit operator UINT64(ulong value)
		{
			return new UINT64(value);
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06005321 RID: 21281 RVA: 0x0023395B File Offset: 0x00231B5B
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x0023395F File Offset: 0x00231B5F
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005028 RID: 20520
		private ulong value;
	}
}
