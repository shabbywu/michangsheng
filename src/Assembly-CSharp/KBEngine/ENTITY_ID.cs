using System;

namespace KBEngine
{
	// Token: 0x02000BBA RID: 3002
	public struct ENTITY_ID
	{
		// Token: 0x060053BC RID: 21436 RVA: 0x00233E21 File Offset: 0x00232021
		private ENTITY_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x00233E2A File Offset: 0x0023202A
		public static implicit operator int(ENTITY_ID value)
		{
			return value.value;
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00233E32 File Offset: 0x00232032
		public static implicit operator ENTITY_ID(int value)
		{
			return new ENTITY_ID(value);
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060053C0 RID: 21440 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005046 RID: 20550
		private int value;
	}
}
