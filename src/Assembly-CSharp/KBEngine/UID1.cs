using System;

namespace KBEngine
{
	// Token: 0x02000BB9 RID: 3001
	public struct UID1
	{
		// Token: 0x060053B7 RID: 21431 RVA: 0x00233DF3 File Offset: 0x00231FF3
		private UID1(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00233DFC File Offset: 0x00231FFC
		public static implicit operator byte[](UID1 value)
		{
			return value.value;
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x00233E04 File Offset: 0x00232004
		public static implicit operator UID1(byte[] value)
		{
			return new UID1(value);
		}

		// Token: 0x1700063D RID: 1597
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

		// Token: 0x04005045 RID: 20549
		private byte[] value;
	}
}
