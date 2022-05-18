using System;

namespace KBEngine
{
	// Token: 0x02000F2A RID: 3882
	public struct PY_DICT
	{
		// Token: 0x06005D8F RID: 23951 RVA: 0x00041C36 File Offset: 0x0003FE36
		private PY_DICT(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x00041C3F File Offset: 0x0003FE3F
		public static implicit operator byte[](PY_DICT value)
		{
			return value.value;
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x00041C47 File Offset: 0x0003FE47
		public static implicit operator PY_DICT(byte[] value)
		{
			return new PY_DICT(value);
		}

		// Token: 0x1700086E RID: 2158
		public byte this[int ID]
		{
			get
			{
				return this.value[ID];
			}
			set
			{
				this.value[ID] = value;
			}
		}

		// Token: 0x04005AD4 RID: 23252
		private byte[] value;
	}
}
