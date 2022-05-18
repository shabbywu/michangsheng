using System;

namespace KBEngine
{
	// Token: 0x02000F35 RID: 3893
	public struct EXPERIENCE
	{
		// Token: 0x06005DD2 RID: 24018 RVA: 0x00041EA5 File Offset: 0x000400A5
		private EXPERIENCE(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x00041EAE File Offset: 0x000400AE
		public static implicit operator int(EXPERIENCE value)
		{
			return value.value;
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x00041EB6 File Offset: 0x000400B6
		public static implicit operator EXPERIENCE(int value)
		{
			return new EXPERIENCE(value);
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06005DD5 RID: 24021 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005ADF RID: 23263
		private int value;
	}
}
