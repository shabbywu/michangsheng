using System;

namespace KBEngine
{
	// Token: 0x02000BBF RID: 3007
	public struct ENTITY_UTYPE
	{
		// Token: 0x060053DD RID: 21469 RVA: 0x00233F40 File Offset: 0x00232140
		private ENTITY_UTYPE(uint value)
		{
			this.value = value;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00233F49 File Offset: 0x00232149
		public static implicit operator uint(ENTITY_UTYPE value)
		{
			return value.value;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x00233F51 File Offset: 0x00232151
		public static implicit operator ENTITY_UTYPE(uint value)
		{
			return new ENTITY_UTYPE(value);
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x0023397C File Offset: 0x00231B7C
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x0000280F File Offset: 0x00000A0F
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x0400504B RID: 20555
		private uint value;
	}
}
