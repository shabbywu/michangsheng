namespace KBEngine;

public class DATATYPE_ITEM_INFO : DATATYPE_BASE
{
	public ITEM_INFO createFromStreamEx(MemoryStream stream)
	{
		return new ITEM_INFO
		{
			UUID = stream.readUint64(),
			itemId = stream.readInt32(),
			itemCount = stream.readUint32(),
			itemIndex = stream.readInt32()
		};
	}

	public void addToStreamEx(Bundle stream, ITEM_INFO v)
	{
		stream.writeUint64(v.UUID);
		stream.writeInt32(v.itemId);
		stream.writeUint32(v.itemCount);
		stream.writeInt32(v.itemIndex);
	}
}
