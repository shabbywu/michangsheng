using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_AnonymousArray_50 : DATATYPE_BASE
{
	public List<ushort> createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		List<ushort> list = new List<ushort>();
		while (num != 0)
		{
			num--;
			list.Add(stream.readUint16());
		}
		return list;
	}

	public void addToStreamEx(Bundle stream, List<ushort> v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeUint16(v[i]);
		}
	}
}
