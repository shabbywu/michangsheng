using System;

namespace KBEngine
{
	// Token: 0x02000F1E RID: 3870
	public struct UINT16
	{
		// Token: 0x06005D57 RID: 23895 RVA: 0x00041A87 File Offset: 0x0003FC87
		private UINT16(ushort value)
		{
			this.value = value;
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x00041A90 File Offset: 0x0003FC90
		public static implicit operator ushort(UINT16 value)
		{
			return value.value;
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x00041A98 File Offset: 0x0003FC98
		public static implicit operator UINT16(ushort value)
		{
			return new UINT16(value);
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x00041AA0 File Offset: 0x0003FCA0
		public static ushort MaxValue
		{
			get
			{
				return ushort.MaxValue;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x00004050 File Offset: 0x00002250
		public static ushort MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005AC8 RID: 23240
		private ushort value;
	}
}
