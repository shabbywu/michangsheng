using System;

namespace KBEngine
{
	// Token: 0x02000F49 RID: 3913
	public struct ENTITY_SUBSTATE
	{
		// Token: 0x06005E3E RID: 24126 RVA: 0x00042150 File Offset: 0x00040350
		private ENTITY_SUBSTATE(byte value)
		{
			this.value = value;
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x00042159 File Offset: 0x00040359
		public static implicit operator byte(ENTITY_SUBSTATE value)
		{
			return value.value;
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x00042161 File Offset: 0x00040361
		public static implicit operator ENTITY_SUBSTATE(byte value)
		{
			return new ENTITY_SUBSTATE(value);
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x00041A80 File Offset: 0x0003FC80
		public static byte MaxValue
		{
			get
			{
				return byte.MaxValue;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x00004050 File Offset: 0x00002250
		public static byte MinValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04005AF3 RID: 23283
		private byte value;
	}
}
