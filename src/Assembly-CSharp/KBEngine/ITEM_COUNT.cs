using System;

namespace KBEngine
{
	// Token: 0x02000BC0 RID: 3008
	public struct ITEM_COUNT
	{
		// Token: 0x060053E2 RID: 21474 RVA: 0x00233F59 File Offset: 0x00232159
		private ITEM_COUNT(uint value)
		{
			this.value = value;
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x00233F62 File Offset: 0x00232162
		public static implicit operator uint(ITEM_COUNT value)
		{
			return value.value;
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x00233F6A File Offset: 0x0023216A
		public static implicit operator ITEM_COUNT(uint value)
		{
			return new ITEM_COUNT(value);
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x0023397C File Offset: 0x00231B7C
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x0000280F File Offset: 0x00000A0F
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x0400504C RID: 20556
		private uint value;
	}
}
