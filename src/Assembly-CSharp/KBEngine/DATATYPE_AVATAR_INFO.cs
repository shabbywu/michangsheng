namespace KBEngine;

public class DATATYPE_AVATAR_INFO : DATATYPE_BASE
{
	public AVATAR_INFO createFromStreamEx(MemoryStream stream)
	{
		return new AVATAR_INFO
		{
			dbid = stream.readUint64(),
			name = stream.readUnicode(),
			roleType = stream.readUint16(),
			level = stream.readUint32()
		};
	}

	public void addToStreamEx(Bundle stream, AVATAR_INFO v)
	{
		stream.writeUint64(v.dbid);
		stream.writeUnicode(v.name);
		stream.writeUint16(v.roleType);
		stream.writeUint32(v.level);
	}
}
