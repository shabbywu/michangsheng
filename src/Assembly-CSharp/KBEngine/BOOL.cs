using System;

namespace KBEngine
{
	// Token: 0x02000BB0 RID: 2992
	public struct BOOL
	{
		// Token: 0x0600538A RID: 21386 RVA: 0x00233D12 File Offset: 0x00231F12
		private BOOL(byte value)
		{
			this.value = value;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x00233D1B File Offset: 0x00231F1B
		public static implicit operator byte(BOOL value)
		{
			return value.value;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00233D23 File Offset: 0x00231F23
		public static implicit operator BOOL(byte value)
		{
			return new BOOL(value);
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x0023391B File Offset: 0x00231B1B
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600538E RID: 21390 RVA: 0x0000280F File Offset: 0x00000A0F
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0400503C RID: 20540
		private byte value;
	}
}
