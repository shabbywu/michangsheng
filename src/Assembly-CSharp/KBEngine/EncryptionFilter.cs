namespace KBEngine;

public abstract class EncryptionFilter
{
	public abstract void encrypt(MemoryStream stream);

	public abstract void decrypt(MemoryStream stream);

	public abstract void decrypt(byte[] buffer, int startIndex, int length);

	public abstract bool send(PacketSenderBase sender, MemoryStream stream);

	public abstract bool recv(MessageReaderBase reader, byte[] buffer, uint rpos, uint len);
}
