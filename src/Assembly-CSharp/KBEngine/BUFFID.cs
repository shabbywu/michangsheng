using System;

namespace KBEngine
{
	// Token: 0x02000F38 RID: 3896
	public struct BUFFID
	{
		// Token: 0x06005DE1 RID: 24033 RVA: 0x00041EF0 File Offset: 0x000400F0
		private BUFFID(ushort value)
		{
			this.value = value;
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x00041EF9 File Offset: 0x000400F9
		public static implicit operator ushort(BUFFID value)
		{
			return value.value;
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x00041F01 File Offset: 0x00040101
		public static implicit operator BUFFID(ushort value)
		{
			return new BUFFID(value);
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x00041AA0 File Offset: 0x0003FCA0
		public static ushort MaxValue
		{
			get
			{
				return ushort.MaxValue;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06005DE5 RID: 24037 RVA: 0x00004050 File Offset: 0x00002250
		public static ushort MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005AE2 RID: 23266
		private ushort value;
	}
}
