using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_AnonymousArray_47 : DATATYPE_BASE
{
	public List<uint> createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		List<uint> list = new List<uint>();
		while (num != 0)
		{
			num--;
			list.Add(stream.readUint32());
		}
		return list;
	}

	public void addToStreamEx(Bundle stream, List<uint> v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeUint32(v[i]);
		}
	}
}
