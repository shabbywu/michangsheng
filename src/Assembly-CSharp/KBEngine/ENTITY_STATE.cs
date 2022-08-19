using System;

namespace KBEngine
{
	// Token: 0x02000BC5 RID: 3013
	public struct ENTITY_STATE
	{
		// Token: 0x060053FB RID: 21499 RVA: 0x00233FD6 File Offset: 0x002321D6
		private ENTITY_STATE(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00233FDF File Offset: 0x002321DF
		public static implicit operator sbyte(ENTITY_STATE value)
		{
			return value.value;
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x00233FE7 File Offset: 0x002321E7
		public static implicit operator ENTITY_STATE(sbyte value)
		{
			return new ENTITY_STATE(value);
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x00233998 File Offset: 0x00231B98
		public static sbyte MaxValue
		{
			get
			{
				return sbyte.MaxValue;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x0023399C File Offset: 0x00231B9C
		public static sbyte MinValue
		{
			get
			{
				return sbyte.MinValue;
			}
		}

		// Token: 0x04005051 RID: 20561
		private sbyte value;
	}
}
