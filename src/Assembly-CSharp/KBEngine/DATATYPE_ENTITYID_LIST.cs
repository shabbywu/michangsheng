namespace KBEngine;

public class DATATYPE_ENTITYID_LIST : DATATYPE_BASE
{
	public ENTITYID_LIST createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		ENTITYID_LIST eNTITYID_LIST = new ENTITYID_LIST();
		while (num != 0)
		{
			num--;
			eNTITYID_LIST.Add(stream.readInt32());
		}
		return eNTITYID_LIST;
	}

	public void addToStreamEx(Bundle stream, ENTITYID_LIST v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeInt32(v[i]);
		}
	}
}
