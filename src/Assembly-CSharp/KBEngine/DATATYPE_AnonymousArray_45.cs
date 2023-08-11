using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_AnonymousArray_45 : DATATYPE_BASE
{
	public List<int> createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		List<int> list = new List<int>();
		while (num != 0)
		{
			num--;
			list.Add(stream.readInt32());
		}
		return list;
	}

	public void addToStreamEx(Bundle stream, List<int> v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeInt32(v[i]);
		}
	}
}
