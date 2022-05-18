using System;

namespace KBEngine
{
	// Token: 0x02000F34 RID: 3892
	public struct CONTROLLER_ID
	{
		// Token: 0x06005DCD RID: 24013 RVA: 0x00041E8C File Offset: 0x0004008C
		private CONTROLLER_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x00041E95 File Offset: 0x00040095
		public static implicit operator int(CONTROLLER_ID value)
		{
			return value.value;
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x00041E9D File Offset: 0x0004009D
		public static implicit operator CONTROLLER_ID(int value)
		{
			return new CONTROLLER_ID(value);
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06005DD1 RID: 24017 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005ADE RID: 23262
		private int value;
	}
}
