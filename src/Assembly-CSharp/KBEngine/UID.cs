using System;

namespace KBEngine
{
	// Token: 0x02000F3B RID: 3899
	public struct UID
	{
		// Token: 0x06005DF0 RID: 24048 RVA: 0x00041F3B File Offset: 0x0004013B
		private UID(ulong value)
		{
			this.value = value;
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x00041F44 File Offset: 0x00040144
		public static implicit operator ulong(UID value)
		{
			return value.value;
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x00041F4C File Offset: 0x0004014C
		public static implicit operator UID(ulong value)
		{
			return new UID(value);
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06005DF3 RID: 24051 RVA: 0x00041AC0 File Offset: 0x0003FCC0
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x00025C53 File Offset: 0x00023E53
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005AE5 RID: 23269
		private ulong value;
	}
}
