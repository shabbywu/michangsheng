using System;

namespace KBEngine
{
	// Token: 0x02000EE6 RID: 3814
	public abstract class EncryptionFilter
	{
		// Token: 0x06005BCB RID: 23499
		public abstract void encrypt(MemoryStream stream);

		// Token: 0x06005BCC RID: 23500
		public abstract void decrypt(MemoryStream stream);

		// Token: 0x06005BCD RID: 23501
		public abstract void decrypt(byte[] buffer, int startIndex, int length);

		// Token: 0x06005BCE RID: 23502
		public abstract bool send(PacketSenderBase sender, MemoryStream stream);

		// Token: 0x06005BCF RID: 23503
		public abstract bool recv(MessageReaderBase reader, byte[] buffer, uint rpos, uint len);
	}
}
