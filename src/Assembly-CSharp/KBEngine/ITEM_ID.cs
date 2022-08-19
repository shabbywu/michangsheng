using System;

namespace KBEngine
{
	// Token: 0x02000BB3 RID: 2995
	public struct ITEM_ID
	{
		// Token: 0x06005399 RID: 21401 RVA: 0x00233D5D File Offset: 0x00231F5D
		private ITEM_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00233D66 File Offset: 0x00231F66
		public static implicit operator int(ITEM_ID value)
		{
			return value.value;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x00233D6E File Offset: 0x00231F6E
		public static implicit operator ITEM_ID(int value)
		{
			return new ITEM_ID(value);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600539C RID: 21404 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0400503F RID: 20543
		private int value;
	}
}
