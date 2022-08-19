using System;

namespace KBEngine
{
	// Token: 0x02000BBB RID: 3003
	public struct ENTITY_NO
	{
		// Token: 0x060053C1 RID: 21441 RVA: 0x00233E3A File Offset: 0x0023203A
		private ENTITY_NO(uint value)
		{
			this.value = value;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00233E43 File Offset: 0x00232043
		public static implicit operator uint(ENTITY_NO value)
		{
			return value.value;
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00233E4B File Offset: 0x0023204B
		public static implicit operator ENTITY_NO(uint value)
		{
			return new ENTITY_NO(value);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060053C4 RID: 21444 RVA: 0x0023397C File Offset: 0x00231B7C
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x0000280F File Offset: 0x00000A0F
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005047 RID: 20551
		private uint value;
	}
}
