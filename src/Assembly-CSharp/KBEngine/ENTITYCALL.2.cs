using System;

namespace KBEngine
{
	// Token: 0x02000F2D RID: 3885
	public struct ENTITYCALL
	{
		// Token: 0x06005D9E RID: 23966 RVA: 0x00041CC0 File Offset: 0x0003FEC0
		private ENTITYCALL(byte[] value)
		{
			this.value = value;
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x00041CC9 File Offset: 0x0003FEC9
		public static implicit operator byte[](ENTITYCALL value)
		{
			return value.value;
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x00041CD1 File Offset: 0x0003FED1
		public static implicit operator ENTITYCALL(byte[] value)
		{
			return new ENTITYCALL(value);
		}

		// Token: 0x17000871 RID: 2161
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

		// Token: 0x04005AD7 RID: 23255
		private byte[] value;
	}
}
