using System;

namespace KBEngine
{
	// Token: 0x02000BA5 RID: 2981
	public struct DOUBLE
	{
		// Token: 0x06005347 RID: 21319 RVA: 0x00233A77 File Offset: 0x00231C77
		private DOUBLE(double value)
		{
			this.value = value;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x00233A80 File Offset: 0x00231C80
		public static implicit operator double(DOUBLE value)
		{
			return value.value;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00233A88 File Offset: 0x00231C88
		public static implicit operator DOUBLE(double value)
		{
			return new DOUBLE(value);
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600534A RID: 21322 RVA: 0x00233A91 File Offset: 0x00231C91
		public static double MaxValue
		{
			get
			{
				return double.MaxValue;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x00233A9C File Offset: 0x00231C9C
		public static double MinValue
		{
			get
			{
				return double.MinValue;
			}
		}

		// Token: 0x04005031 RID: 20529
		private double value;
	}
}
