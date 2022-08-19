using System;

namespace KBEngine
{
	// Token: 0x02000B69 RID: 2921
	public abstract class EncryptionFilter
	{
		// Token: 0x0600518F RID: 20879
		public abstract void encrypt(MemoryStream stream);

		// Token: 0x06005190 RID: 20880
		public abstract void decrypt(MemoryStream stream);

		// Token: 0x06005191 RID: 20881
		public abstract void decrypt(byte[] buffer, int startIndex, int length);

		// Token: 0x06005192 RID: 20882
		public abstract bool send(PacketSenderBase sender, MemoryStream stream);

		// Token: 0x06005193 RID: 20883
		public abstract bool recv(MessageReaderBase reader, byte[] buffer, uint rpos, uint len);
	}
}
