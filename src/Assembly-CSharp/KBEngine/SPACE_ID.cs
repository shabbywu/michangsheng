using System;

namespace KBEngine
{
	// Token: 0x02000F3F RID: 3903
	public struct SPACE_ID
	{
		// Token: 0x06005E04 RID: 24068 RVA: 0x00041FB4 File Offset: 0x000401B4
		private SPACE_ID(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x00041FBD File Offset: 0x000401BD
		public static implicit operator uint(SPACE_ID value)
		{
			return value.value;
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x00041FC5 File Offset: 0x000401C5
		public static implicit operator SPACE_ID(uint value)
		{
			return new SPACE_ID(value);
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x00004050 File Offset: 0x00002250
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005AE9 RID: 23273
		private uint value;
	}
}
