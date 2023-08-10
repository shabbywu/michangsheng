using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_ITEM_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_ITEM_INFO itemType = new DATATYPE_ITEM_INFO();

		public List<ITEM_INFO> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<ITEM_INFO> list = new List<ITEM_INFO>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<ITEM_INFO> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray();

	public ITEM_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new ITEM_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, ITEM_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
