using System;

namespace KBEngine
{
	// Token: 0x02000F45 RID: 3909
	public struct ENMITY
	{
		// Token: 0x06005E2A RID: 24106 RVA: 0x000420EC File Offset: 0x000402EC
		private ENMITY(int value)
		{
			this.value = value;
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x000420F5 File Offset: 0x000402F5
		public static implicit operator int(ENMITY value)
		{
			return value.value;
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x000420FD File Offset: 0x000402FD
		public static implicit operator ENMITY(int value)
		{
			return new ENMITY(value);
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06005E2D RID: 24109 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06005E2E RID: 24110 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AEF RID: 23279
		private int value;
	}
}
