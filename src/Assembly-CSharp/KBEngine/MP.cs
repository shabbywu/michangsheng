using System;

namespace KBEngine
{
	// Token: 0x02000F47 RID: 3911
	public struct MP
	{
		// Token: 0x06005E34 RID: 24116 RVA: 0x0004211E File Offset: 0x0004031E
		private MP(int value)
		{
			this.value = value;
		}

		// Token: 0x06005E35 RID: 24117 RVA: 0x00042127 File Offset: 0x00040327
		public static implicit operator int(MP value)
		{
			return value.value;
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x0004212F File Offset: 0x0004032F
		public static implicit operator MP(int value)
		{
			return new MP(value);
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06005E38 RID: 24120 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AF1 RID: 23281
		private int value;
	}
}
