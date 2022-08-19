using System;

namespace KBEngine
{
	// Token: 0x02000BAB RID: 2987
	public struct BLOB
	{
		// Token: 0x06005365 RID: 21349 RVA: 0x00233B8D File Offset: 0x00231D8D
		private BLOB(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x00233B96 File Offset: 0x00231D96
		public static implicit operator byte[](BLOB value)
		{
			return value.value;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00233B9E File Offset: 0x00231D9E
		public static implicit operator BLOB(byte[] value)
		{
			return new BLOB(value);
		}

		// Token: 0x1700061F RID: 1567
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

		// Token: 0x04005037 RID: 20535
		private byte[] value;
	}
}
