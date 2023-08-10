namespace KBEngine;

public class DATATYPE_FRIEND_INFO : DATATYPE_BASE
{
	public FRIEND_INFO createFromStreamEx(MemoryStream stream)
	{
		return new FRIEND_INFO
		{
			dbid = stream.readUint64(),
			name = stream.readUnicode(),
			level = stream.readUint32()
		};
	}

	public void addToStreamEx(Bundle stream, FRIEND_INFO v)
	{
		stream.writeUint64(v.dbid);
		stream.writeUnicode(v.name);
		stream.writeUint32(v.level);
	}
}
