using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_AnonymousArray_48 : DATATYPE_BASE
{
	public List<string> createFromStreamEx(MemoryStream stream)
	{
		uint num = stream.readUint32();
		List<string> list = new List<string>();
		while (num != 0)
		{
			num--;
			list.Add(stream.readUnicode());
		}
		return list;
	}

	public void addToStreamEx(Bundle stream, List<string> v)
	{
		stream.writeUint32((uint)v.Count);
		for (int i = 0; i < v.Count; i++)
		{
			stream.writeUnicode(v[i]);
		}
	}
}
