using System;

namespace KBEngine
{
	// Token: 0x02000F3E RID: 3902
	public struct ENTITY_NO
	{
		// Token: 0x06005DFF RID: 24063 RVA: 0x00041F9B File Offset: 0x0004019B
		private ENTITY_NO(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x00041FA4 File Offset: 0x000401A4
		public static implicit operator uint(ENTITY_NO value)
		{
			return value.value;
		}

		// Token: 0x06005E01 RID: 24065 RVA: 0x00041FAC File Offset: 0x000401AC
		public static implicit operator ENTITY_NO(uint value)
		{
			return new ENTITY_NO(value);
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x00004050 File Offset: 0x00002250
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005AE8 RID: 23272
		private uint value;
	}
}
