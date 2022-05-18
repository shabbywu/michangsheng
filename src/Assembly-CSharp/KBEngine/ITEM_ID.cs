using System;

namespace KBEngine
{
	// Token: 0x02000F36 RID: 3894
	public struct ITEM_ID
	{
		// Token: 0x06005DD7 RID: 24023 RVA: 0x00041EBE File Offset: 0x000400BE
		private ITEM_ID(int value)
		{
			this.value = value;
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x00041EC7 File Offset: 0x000400C7
		public static implicit operator int(ITEM_ID value)
		{
			return value.value;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x00041ECF File Offset: 0x000400CF
		public static implicit operator ITEM_ID(int value)
		{
			return new ITEM_ID(value);
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AE0 RID: 23264
		private int value;
	}
}
