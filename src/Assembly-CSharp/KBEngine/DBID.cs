using System;

namespace KBEngine
{
	// Token: 0x02000BB7 RID: 2999
	public struct DBID
	{
		// Token: 0x060053AD RID: 21421 RVA: 0x00233DC1 File Offset: 0x00231FC1
		private DBID(ulong value)
		{
			this.value = value;
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00233DCA File Offset: 0x00231FCA
		public static implicit operator ulong(DBID value)
		{
			return value.value;
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00233DD2 File Offset: 0x00231FD2
		public static implicit operator DBID(ulong value)
		{
			return new DBID(value);
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x0023395B File Offset: 0x00231B5B
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0023395F File Offset: 0x00231B5F
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005043 RID: 20547
		private ulong value;
	}
}
