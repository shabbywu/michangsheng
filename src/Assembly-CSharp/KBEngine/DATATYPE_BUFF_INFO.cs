namespace KBEngine;

public class DATATYPE_BUFF_INFO : DATATYPE_BASE
{
	public BUFF_INFO createFromStreamEx(MemoryStream stream)
	{
		return new BUFF_INFO
		{
			buffid = stream.readUint32()
		};
	}

	public void addToStreamEx(Bundle stream, BUFF_INFO v)
	{
		stream.writeUint32(v.buffid);
	}
}
