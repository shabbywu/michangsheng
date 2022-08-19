using System;

namespace KBEngine
{
	// Token: 0x02000B9D RID: 2973
	public struct UINT32
	{
		// Token: 0x06005323 RID: 21283 RVA: 0x00233963 File Offset: 0x00231B63
		private UINT32(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0023396C File Offset: 0x00231B6C
		public static implicit operator uint(UINT32 value)
		{
			return value.value;
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00233974 File Offset: 0x00231B74
		public static implicit operator UINT32(uint value)
		{
			return new UINT32(value);
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x0023397C File Offset: 0x00231B7C
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06005327 RID: 21287 RVA: 0x0000280F File Offset: 0x00000A0F
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005029 RID: 20521
		private uint value;
	}
}
