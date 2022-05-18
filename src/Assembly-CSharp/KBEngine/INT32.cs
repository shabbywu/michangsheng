using System;

namespace KBEngine
{
	// Token: 0x02000F23 RID: 3875
	public struct INT32
	{
		// Token: 0x06005D70 RID: 23920 RVA: 0x00041B28 File Offset: 0x0003FD28
		private INT32(int value)
		{
			this.value = value;
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x00041B31 File Offset: 0x0003FD31
		public static implicit operator int(INT32 value)
		{
			return value.value;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00041B39 File Offset: 0x0003FD39
		public static implicit operator INT32(int value)
		{
			return new INT32(value);
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005ACD RID: 23245
		private int value;
	}
}
