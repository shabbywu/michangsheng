using System;

namespace KBEngine
{
	// Token: 0x02000BA8 RID: 2984
	public struct PY_TUPLE
	{
		// Token: 0x06005356 RID: 21334 RVA: 0x00233B03 File Offset: 0x00231D03
		private PY_TUPLE(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00233B0C File Offset: 0x00231D0C
		public static implicit operator byte[](PY_TUPLE value)
		{
			return value.value;
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x00233B14 File Offset: 0x00231D14
		public static implicit operator PY_TUPLE(byte[] value)
		{
			return new PY_TUPLE(value);
		}

		// Token: 0x1700061C RID: 1564
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

		// Token: 0x04005034 RID: 20532
		private byte[] value;
	}
}
