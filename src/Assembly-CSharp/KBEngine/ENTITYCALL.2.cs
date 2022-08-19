using System;

namespace KBEngine
{
	// Token: 0x02000BAA RID: 2986
	public struct ENTITYCALL
	{
		// Token: 0x06005360 RID: 21344 RVA: 0x00233B5F File Offset: 0x00231D5F
		private ENTITYCALL(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00233B68 File Offset: 0x00231D68
		public static implicit operator byte[](ENTITYCALL value)
		{
			return value.value;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00233B70 File Offset: 0x00231D70
		public static implicit operator ENTITYCALL(byte[] value)
		{
			return new ENTITYCALL(value);
		}

		// Token: 0x1700061E RID: 1566
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

		// Token: 0x04005036 RID: 20534
		private byte[] value;
	}
}
