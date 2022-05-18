using System;

namespace KBEngine
{
	// Token: 0x02000F32 RID: 3890
	public struct OBJECT_ID
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x00041E5A File Offset: 0x0004005A
		private OBJECT_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x00041E63 File Offset: 0x00040063
		public static implicit operator int(OBJECT_ID value)
		{
			return value.value;
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x00041E6B File Offset: 0x0004006B
		public static implicit operator OBJECT_ID(int value)
		{
			return new OBJECT_ID(value);
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005ADC RID: 23260
		private int value;
	}
}
