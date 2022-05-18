using System;

namespace KBEngine
{
	// Token: 0x02000F29 RID: 3881
	public struct PYTHON
	{
		// Token: 0x06005D8A RID: 23946 RVA: 0x00041C08 File Offset: 0x0003FE08
		private PYTHON(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x00041C11 File Offset: 0x0003FE11
		public static implicit operator byte[](PYTHON value)
		{
			return value.value;
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x00041C19 File Offset: 0x0003FE19
		public static implicit operator PYTHON(byte[] value)
		{
			return new PYTHON(value);
		}

		// Token: 0x1700086D RID: 2157
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

		// Token: 0x04005AD3 RID: 23251
		private byte[] value;
	}
}
