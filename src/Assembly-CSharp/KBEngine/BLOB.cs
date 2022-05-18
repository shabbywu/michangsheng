using System;

namespace KBEngine
{
	// Token: 0x02000F2E RID: 3886
	public struct BLOB
	{
		// Token: 0x06005DA3 RID: 23971 RVA: 0x00041CEE File Offset: 0x0003FEEE
		private BLOB(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x00041CF7 File Offset: 0x0003FEF7
		public static implicit operator byte[](BLOB value)
		{
			return value.value;
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x00041CFF File Offset: 0x0003FEFF
		public static implicit operator BLOB(byte[] value)
		{
			return new BLOB(value);
		}

		// Token: 0x17000872 RID: 2162
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

		// Token: 0x04005AD8 RID: 23256
		private byte[] value;
	}
}
