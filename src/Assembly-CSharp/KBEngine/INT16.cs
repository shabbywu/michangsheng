using System;

namespace KBEngine
{
	// Token: 0x02000F22 RID: 3874
	public struct INT16
	{
		// Token: 0x06005D6B RID: 23915 RVA: 0x00041B01 File Offset: 0x0003FD01
		private INT16(short value)
		{
			this.value = value;
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x00041B0A File Offset: 0x0003FD0A
		public static implicit operator short(INT16 value)
		{
			return value.value;
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x00041B12 File Offset: 0x0003FD12
		public static implicit operator INT16(short value)
		{
			return new INT16(value);
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x00041B1A File Offset: 0x0003FD1A
		public static short MaxValue
		{
			get
			{
				return short.MaxValue;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x00041B21 File Offset: 0x0003FD21
		public static short MinValue
		{
			get
			{
				return short.MinValue;
			}
		}

		// Token: 0x04005ACC RID: 23244
		private short value;
	}
}
