using System;

namespace KBEngine
{
	// Token: 0x02000F28 RID: 3880
	public struct DOUBLE
	{
		// Token: 0x06005D85 RID: 23941 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		private DOUBLE(double value)
		{
			this.value = value;
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x00041BE1 File Offset: 0x0003FDE1
		public static implicit operator double(DOUBLE value)
		{
			return value.value;
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x00041BE9 File Offset: 0x0003FDE9
		public static implicit operator DOUBLE(double value)
		{
			return new DOUBLE(value);
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x00041BF2 File Offset: 0x0003FDF2
		public static double MaxValue
		{
			get
			{
				return double.MaxValue;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x00041BFD File Offset: 0x0003FDFD
		public static double MinValue
		{
			get
			{
				return double.MinValue;
			}
		}

		// Token: 0x04005AD2 RID: 23250
		private double value;
	}
}
