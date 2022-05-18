using System;

namespace KBEngine
{
	// Token: 0x02000F39 RID: 3897
	public struct QUESTID
	{
		// Token: 0x06005DE6 RID: 24038 RVA: 0x00041F09 File Offset: 0x00040109
		private QUESTID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x00041F12 File Offset: 0x00040112
		public static implicit operator int(QUESTID value)
		{
			return value.value;
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x00041F1A File Offset: 0x0004011A
		public static implicit operator QUESTID(int value)
		{
			return new QUESTID(value);
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AE3 RID: 23267
		private int value;
	}
}
