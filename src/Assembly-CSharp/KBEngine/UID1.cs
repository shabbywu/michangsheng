using System;

namespace KBEngine
{
	// Token: 0x02000F3C RID: 3900
	public struct UID1
	{
		// Token: 0x06005DF5 RID: 24053 RVA: 0x00041F54 File Offset: 0x00040154
		private UID1(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x00041F5D File Offset: 0x0004015D
		public static implicit operator byte[](UID1 value)
		{
			return value.value;
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x00041F65 File Offset: 0x00040165
		public static implicit operator UID1(byte[] value)
		{
			return new UID1(value);
		}

		// Token: 0x17000890 RID: 2192
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

		// Token: 0x04005AE6 RID: 23270
		private byte[] value;
	}
}
