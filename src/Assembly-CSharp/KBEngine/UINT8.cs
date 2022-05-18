using System;

namespace KBEngine
{
	// Token: 0x02000F1D RID: 3869
	public struct UINT8
	{
		// Token: 0x06005D52 RID: 23890 RVA: 0x00041A67 File Offset: 0x0003FC67
		private UINT8(byte value)
		{
			this.value = value;
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00041A70 File Offset: 0x0003FC70
		public static implicit operator byte(UINT8 value)
		{
			return value.value;
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x00041A78 File Offset: 0x0003FC78
		public static implicit operator UINT8(byte value)
		{
			return new UINT8(value);
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06005D55 RID: 23893 RVA: 0x00041A80 File Offset: 0x0003FC80
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x00004050 File Offset: 0x00002250
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005AC7 RID: 23239
		private byte value;
	}
}
