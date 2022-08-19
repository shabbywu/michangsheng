using System;

namespace KBEngine
{
	// Token: 0x02000BB5 RID: 2997
	public struct BUFFID
	{
		// Token: 0x060053A3 RID: 21411 RVA: 0x00233D8F File Offset: 0x00231F8F
		private BUFFID(ushort value)
		{
			this.value = value;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00233D98 File Offset: 0x00231F98
		public static implicit operator ushort(BUFFID value)
		{
			return value.value;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00233DA0 File Offset: 0x00231FA0
		public static implicit operator BUFFID(ushort value)
		{
			return new BUFFID(value);
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060053A6 RID: 21414 RVA: 0x0023393B File Offset: 0x00231B3B
		public static ushort MaxValue
		{
			get
			{
				return ushort.MaxValue;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x0000280F File Offset: 0x00000A0F
		public static ushort MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005041 RID: 20545
		private ushort value;
	}
}
