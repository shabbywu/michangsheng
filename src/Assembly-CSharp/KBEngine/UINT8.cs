using System;

namespace KBEngine
{
	// Token: 0x02000B9A RID: 2970
	public struct UINT8
	{
		// Token: 0x06005314 RID: 21268 RVA: 0x00233902 File Offset: 0x00231B02
		private UINT8(byte value)
		{
			this.value = value;
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0023390B File Offset: 0x00231B0B
		public static implicit operator byte(UINT8 value)
		{
			return value.value;
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00233913 File Offset: 0x00231B13
		public static implicit operator UINT8(byte value)
		{
			return new UINT8(value);
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06005317 RID: 21271 RVA: 0x0023391B File Offset: 0x00231B1B
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x0000280F File Offset: 0x00000A0F
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005026 RID: 20518
		private byte value;
	}
}
