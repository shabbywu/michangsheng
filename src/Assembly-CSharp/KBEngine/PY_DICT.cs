using System;

namespace KBEngine
{
	// Token: 0x02000BA7 RID: 2983
	public struct PY_DICT
	{
		// Token: 0x06005351 RID: 21329 RVA: 0x00233AD5 File Offset: 0x00231CD5
		private PY_DICT(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x00233ADE File Offset: 0x00231CDE
		public static implicit operator byte[](PY_DICT value)
		{
			return value.value;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00233AE6 File Offset: 0x00231CE6
		public static implicit operator PY_DICT(byte[] value)
		{
			return new PY_DICT(value);
		}

		// Token: 0x1700061B RID: 1563
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

		// Token: 0x04005033 RID: 20531
		private byte[] value;
	}
}
