namespace KBEngine;

public class DATATYPE_PLAYER_INFO : DATATYPE_BASE
{
	public PLAYER_INFO createFromStreamEx(MemoryStream stream)
	{
		return new PLAYER_INFO
		{
			dbid = stream.readUint64(),
			name = stream.readUnicode(),
			roleType = stream.readUint8(),
			level = stream.readUint16(),
			Exp = stream.readUint16(),
			jade = stream.readUint16(),
			gold = stream.readUint16(),
			soul = stream.readUint16()
		};
	}

	public void addToStreamEx(Bundle stream, PLAYER_INFO v)
	{
		stream.writeUint64(v.dbid);
		stream.writeUnicode(v.name);
		stream.writeUint8(v.roleType);
		stream.writeUint16(v.level);
		stream.writeUint16(v.Exp);
		stream.writeUint16(v.jade);
		stream.writeUint16(v.gold);
		stream.writeUint16(v.soul);
	}
}
