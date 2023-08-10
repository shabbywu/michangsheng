using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_FRIEND_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_FRIEND_INFO itemType = new DATATYPE_FRIEND_INFO();

		public List<FRIEND_INFO> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<FRIEND_INFO> list = new List<FRIEND_INFO>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<FRIEND_INFO> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray();

	public FRIEND_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new FRIEND_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, FRIEND_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
