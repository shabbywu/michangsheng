namespace KBEngine;

public class DATATYPE_ENTITY_FORBID_COUNTER : DATATYPE_BASE
{
	public ENTITY_FORBID_COUNTER createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		ENTITY_FORBID_COUNTER eNTITY_FORBID_COUNTER = new ENTITY_FORBID_COUNTER();
		while (num != 0)
		{
			num--;
			eNTITY_FORBID_COUNTER.Add(stream.readInt8());
		}
		return eNTITY_FORBID_COUNTER;
	}

	public void addToStreamEx(Bundle stream, ENTITY_FORBID_COUNTER v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeInt8(v[i]);
		}
	}
}
