using System;

namespace KBEngine
{
	// Token: 0x02000F44 RID: 3908
	public struct DAMAGE_TYPE
	{
		// Token: 0x06005E25 RID: 24101 RVA: 0x000420D3 File Offset: 0x000402D3
		private DAMAGE_TYPE(int value)
		{
			this.value = value;
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x000420DC File Offset: 0x000402DC
		public static implicit operator int(DAMAGE_TYPE value)
		{
			return value.value;
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x000420E4 File Offset: 0x000402E4
		public static implicit operator DAMAGE_TYPE(int value)
		{
			return new DAMAGE_TYPE(value);
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06005E28 RID: 24104 RVA: 0x00041B41 File Offset: 0x0003FD41
		public static int MaxValue
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x00041B48 File Offset: 0x0003FD48
		public static int MinValue
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x04005AEE RID: 23278
		private int value;
	}
}
