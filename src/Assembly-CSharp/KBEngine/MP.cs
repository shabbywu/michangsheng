using System;

namespace KBEngine
{
	// Token: 0x02000BC4 RID: 3012
	public struct MP
	{
		// Token: 0x060053F6 RID: 21494 RVA: 0x00233FBD File Offset: 0x002321BD
		private MP(int value)
		{
			this.value = value;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00233FC6 File Offset: 0x002321C6
		public static implicit operator int(MP value)
		{
			return value.value;
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00233FCE File Offset: 0x002321CE
		public static implicit operator MP(int value)
		{
			return new MP(value);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x002339E0 File Offset: 0x00231BE0
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x002339E7 File Offset: 0x00231BE7
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005050 RID: 20560
		private int value;
	}
}
