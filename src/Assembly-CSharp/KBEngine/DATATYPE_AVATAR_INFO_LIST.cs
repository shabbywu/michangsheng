using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_AVATAR_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_AVATAR_INFO itemType = new DATATYPE_AVATAR_INFO();

		public List<AVATAR_INFO> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<AVATAR_INFO> list = new List<AVATAR_INFO>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<AVATAR_INFO> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray();

	public AVATAR_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new AVATAR_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, AVATAR_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
