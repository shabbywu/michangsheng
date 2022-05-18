using System;

namespace KBEngine
{
	// Token: 0x02000F24 RID: 3876
	public struct INT64
	{
		// Token: 0x06005D75 RID: 23925 RVA: 0x00041B4F File Offset: 0x0003FD4F
		private INT64(long value)
		{
			this.value = value;
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x00041B58 File Offset: 0x0003FD58
		public static implicit operator long(INT64 value)
		{
			return value.value;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x00041B60 File Offset: 0x0003FD60
		public static implicit operator INT64(long value)
		{
			return new INT64(value);
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x00041B68 File Offset: 0x0003FD68
		public static long MaxValue
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x00041B73 File Offset: 0x0003FD73
		public static long MinValue
		{
			get
			{
				return long.MinValue;
			}
		}

		// Token: 0x04005ACE RID: 23246
		private long value;
	}
}
