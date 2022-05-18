using System;

namespace KBEngine
{
	// Token: 0x02000F3A RID: 3898
	public struct DBID
	{
		// Token: 0x06005DEB RID: 24043 RVA: 0x00041F22 File Offset: 0x00040122
		private DBID(ulong value)
		{
			this.value = value;
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x00041F2B File Offset: 0x0004012B
		public static implicit operator ulong(DBID value)
		{
			return value.value;
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x00041F33 File Offset: 0x00040133
		public static implicit operator DBID(ulong value)
		{
			return new DBID(value);
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x00041AC0 File Offset: 0x0003FCC0
		public static ulong MaxValue
		{
			get
			{
				return ulong.MaxValue;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06005DEF RID: 24047 RVA: 0x00025C53 File Offset: 0x00023E53
		public static ulong MinValue
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x04005AE4 RID: 23268
		private ulong value;
	}
}
