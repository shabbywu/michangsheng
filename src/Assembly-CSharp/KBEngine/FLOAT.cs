using System;

namespace KBEngine
{
	// Token: 0x02000F27 RID: 3879
	public struct FLOAT
	{
		// Token: 0x06005D80 RID: 23936 RVA: 0x00041BB0 File Offset: 0x0003FDB0
		private FLOAT(float value)
		{
			this.value = value;
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x00041BB9 File Offset: 0x0003FDB9
		public static implicit operator float(FLOAT value)
		{
			return value.value;
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x00041BC1 File Offset: 0x0003FDC1
		public static implicit operator FLOAT(float value)
		{
			return new FLOAT(value);
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x00041BCA File Offset: 0x0003FDCA
		public static float MaxValue
		{
			get
			{
				return float.MaxValue;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x00041BD1 File Offset: 0x0003FDD1
		public static float MinValue
		{
			get
			{
				return float.MinValue;
			}
		}

		// Token: 0x04005AD1 RID: 23249
		private float value;
	}
}
