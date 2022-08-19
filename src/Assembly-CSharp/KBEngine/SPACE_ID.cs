using System;

namespace KBEngine
{
	// Token: 0x02000BBC RID: 3004
	public struct SPACE_ID
	{
		// Token: 0x060053C6 RID: 21446 RVA: 0x00233E53 File Offset: 0x00232053
		private SPACE_ID(uint value)
		{
			this.value = value;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00233E5C File Offset: 0x0023205C
		public static implicit operator uint(SPACE_ID value)
		{
			return value.value;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00233E64 File Offset: 0x00232064
		public static implicit operator SPACE_ID(uint value)
		{
			return new SPACE_ID(value);
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060053C9 RID: 21449 RVA: 0x0023397C File Offset: 0x00231B7C
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060053CA RID: 21450 RVA: 0x0000280F File Offset: 0x00000A0F
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005048 RID: 20552
		private uint value;
	}
}
