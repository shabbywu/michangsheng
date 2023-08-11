namespace KBEngine;

public abstract class MessageReaderBase
{
	public abstract void process(byte[] datas, uint offset, uint length);
}
