using System;

namespace KBEngine
{
	// Token: 0x02000F42 RID: 3906
	public struct ENTITY_UTYPE
	{
		// Token: 0x06005E1B RID: 24091 RVA: 0x000420A1 File Offset: 0x000402A1
		private ENTITY_UTYPE(uint value)
		{
			this.value = value;
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x000420AA File Offset: 0x000402AA
		public static implicit operator uint(ENTITY_UTYPE value)
		{
			return value.value;
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x000420B2 File Offset: 0x000402B2
		public static implicit operator ENTITY_UTYPE(uint value)
		{
			return new ENTITY_UTYPE(value);
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public static uint MaxValue
		{
			get
			{
				return uint.MaxValue;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x00004050 File Offset: 0x00002250
		public static uint MinValue
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x04005AEC RID: 23276
		private uint value;
	}
}
