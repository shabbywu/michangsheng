using System;

namespace KBEngine
{
	// Token: 0x02000F43 RID: 3907
	public struct ITEM_COUNT
	{
		// Token: 0x06005E20 RID: 24096 RVA: 0x000420BA File Offset: 0x000402BA
		private ITEM_COUNT(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x000420C3 File Offset: 0x000402C3
		public static implicit operator uint(ITEM_COUNT value)
		{
			return value.value;
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x000420CB File Offset: 0x000402CB
		public static implicit operator ITEM_COUNT(uint value)
		{
			return new ITEM_COUNT(value);
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06005E23 RID: 24099 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x00004050 File Offset: 0x00002250
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005AED RID: 23277
		private uint value;
	}
}
