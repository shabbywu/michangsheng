using System;

namespace KBEngine
{
	// Token: 0x02000B9F RID: 2975
	public struct INT16
	{
		// Token: 0x0600532D RID: 21293 RVA: 0x002339A0 File Offset: 0x00231BA0
		private INT16(short value)
		{
			this.value = value;
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x002339A9 File Offset: 0x00231BA9
		public static implicit operator short(INT16 value)
		{
			return value.value;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x002339B1 File Offset: 0x00231BB1
		public static implicit operator INT16(short value)
		{
			return new INT16(value);
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x002339B9 File Offset: 0x00231BB9
		public static short MaxValue
		{
			get
			{
				return short.MaxValue;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x002339C0 File Offset: 0x00231BC0
		public static short MinValue
		{
			get
			{
				return short.MinValue;
			}
		}

		// Token: 0x0400502B RID: 20523
		private short value;
	}
}
