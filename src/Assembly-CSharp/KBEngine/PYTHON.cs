using System;

namespace KBEngine
{
	// Token: 0x02000BA6 RID: 2982
	public struct PYTHON
	{
		// Token: 0x0600534C RID: 21324 RVA: 0x00233AA7 File Offset: 0x00231CA7
		private PYTHON(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00233AB0 File Offset: 0x00231CB0
		public static implicit operator byte[](PYTHON value)
		{
			return value.value;
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00233AB8 File Offset: 0x00231CB8
		public static implicit operator PYTHON(byte[] value)
		{
			return new PYTHON(value);
		}

		// Token: 0x1700061A RID: 1562
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

		// Token: 0x04005032 RID: 20530
		private byte[] value;
	}
}
