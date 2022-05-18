using System;

namespace KBEngine
{
	// Token: 0x02000F33 RID: 3891
	public struct BOOL
	{
		// Token: 0x06005DC8 RID: 24008 RVA: 0x00041E73 File Offset: 0x00040073
		private BOOL(byte value)
		{
			this.value = value;
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x00041E7C File Offset: 0x0004007C
		public static implicit operator byte(BOOL value)
		{
			return value.value;
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x00041E84 File Offset: 0x00040084
		public static implicit operator BOOL(byte value)
		{
			return new BOOL(value);
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06005DCB RID: 24011 RVA: 0x00041A80 File Offset: 0x0003FC80
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x00004050 File Offset: 0x00002250
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005ADD RID: 23261
		private byte value;
	}
}
