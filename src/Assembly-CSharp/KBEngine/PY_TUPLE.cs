using System;

namespace KBEngine
{
	// Token: 0x02000F2B RID: 3883
	public struct PY_TUPLE
	{
		// Token: 0x06005D94 RID: 23956 RVA: 0x00041C64 File Offset: 0x0003FE64
		private PY_TUPLE(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x00041C6D File Offset: 0x0003FE6D
		public static implicit operator byte[](PY_TUPLE value)
		{
			return value.value;
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x00041C75 File Offset: 0x0003FE75
		public static implicit operator PY_TUPLE(byte[] value)
		{
			return new PY_TUPLE(value);
		}

		// Token: 0x1700086F RID: 2159
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

		// Token: 0x04005AD5 RID: 23253
		private byte[] value;
	}
}
