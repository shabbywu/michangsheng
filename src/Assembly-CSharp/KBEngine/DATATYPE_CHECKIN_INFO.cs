namespace KBEngine;

public class DATATYPE_CHECKIN_INFO : DATATYPE_BASE
{
	public CHECKIN_INFO createFromStreamEx(MemoryStream stream)
	{
		return new CHECKIN_INFO
		{
			type = stream.readUint16(),
			count = stream.readUint16(),
			time = stream.readUint32()
		};
	}

	public void addToStreamEx(Bundle stream, CHECKIN_INFO v)
	{
		stream.writeUint16(v.type);
		stream.writeUint16(v.count);
		stream.writeUint32(v.time);
	}
}
