using System;

namespace KBEngine
{
	// Token: 0x02000F20 RID: 3872
	public struct UINT32
	{
		// Token: 0x06005D61 RID: 23905 RVA: 0x00041AC4 File Offset: 0x0003FCC4
		private UINT32(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x00041ACD File Offset: 0x0003FCCD
		public static implicit operator uint(UINT32 value)
		{
			return value.value;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00041AD5 File Offset: 0x0003FCD5
		public static implicit operator UINT32(uint value)
		{
			return new UINT32(value);
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06005D64 RID: 23908 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06005D65 RID: 23909 RVA: 0x00004050 File Offset: 0x00002250
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005ACA RID: 23242
		private uint value;
	}
}
