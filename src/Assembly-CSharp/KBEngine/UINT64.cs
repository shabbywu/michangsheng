using System;

namespace KBEngine
{
	// Token: 0x02000F1F RID: 3871
	public struct UINT64
	{
		// Token: 0x06005D5C RID: 23900 RVA: 0x00041AA7 File Offset: 0x0003FCA7
		private UINT64(ulong value)
		{
			this.value = value;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x00041AB0 File Offset: 0x0003FCB0
		public static implicit operator ulong(UINT64 value)
		{
			return value.value;
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x00041AB8 File Offset: 0x0003FCB8
		public static implicit operator UINT64(ulong value)
		{
			return new UINT64(value);
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06005D5F RID: 23903 RVA: 0x00041AC0 File Offset: 0x0003FCC0
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06005D60 RID: 23904 RVA: 0x00025C53 File Offset: 0x00023E53
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005AC9 RID: 23241
		private ulong value;
	}
}
