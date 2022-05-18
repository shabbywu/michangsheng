using System;

namespace KBEngine
{
	// Token: 0x02000F2C RID: 3884
	public struct PY_LIST
	{
		// Token: 0x06005D99 RID: 23961 RVA: 0x00041C92 File Offset: 0x0003FE92
		private PY_LIST(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x00041C9B File Offset: 0x0003FE9B
		public static implicit operator byte[](PY_LIST value)
		{
			return value.value;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x00041CA3 File Offset: 0x0003FEA3
		public static implicit operator PY_LIST(byte[] value)
		{
			return new PY_LIST(value);
		}

		// Token: 0x17000870 RID: 2160
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

		// Token: 0x04005AD6 RID: 23254
		private byte[] value;
	}
}
