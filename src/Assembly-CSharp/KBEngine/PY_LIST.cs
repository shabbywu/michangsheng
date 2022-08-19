using System;

namespace KBEngine
{
	// Token: 0x02000BA9 RID: 2985
	public struct PY_LIST
	{
		// Token: 0x0600535B RID: 21339 RVA: 0x00233B31 File Offset: 0x00231D31
		private PY_LIST(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x00233B3A File Offset: 0x00231D3A
		public static implicit operator byte[](PY_LIST value)
		{
			return value.value;
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00233B42 File Offset: 0x00231D42
		public static implicit operator PY_LIST(byte[] value)
		{
			return new PY_LIST(value);
		}

		// Token: 0x1700061D RID: 1565
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

		// Token: 0x04005035 RID: 20533
		private byte[] value;
	}
}
