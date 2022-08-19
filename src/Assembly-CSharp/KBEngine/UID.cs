using System;

namespace KBEngine
{
	// Token: 0x02000BB8 RID: 3000
	public struct UID
	{
		// Token: 0x060053B2 RID: 21426 RVA: 0x00233DDA File Offset: 0x00231FDA
		private UID(ulong value)
		{
			this.value = value;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00233DE3 File Offset: 0x00231FE3
		public static implicit operator ulong(UID value)
		{
			return value.value;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x00233DEB File Offset: 0x00231FEB
		public static implicit operator UID(ulong value)
		{
			return new UID(value);
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060053B5 RID: 21429 RVA: 0x0023395B File Offset: 0x00231B5B
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x0023395F File Offset: 0x00231B5F
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005044 RID: 20548
		private ulong value;
	}
}
