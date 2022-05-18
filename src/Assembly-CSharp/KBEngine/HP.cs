using System;

namespace KBEngine
{
	// Token: 0x02000F46 RID: 3910
	public struct HP
	{
		// Token: 0x06005E2F RID: 24111 RVA: 0x00042105 File Offset: 0x00040305
		private HP(int value)
		{
			this.value = value;
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0004210E File Offset: 0x0004030E
		public static implicit operator int(HP value)
		{
			return value.value;
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x00042116 File Offset: 0x00040316
		public static implicit operator HP(int value)
		{
			return new HP(value);
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AF0 RID: 23280
		private int value;
	}
}
