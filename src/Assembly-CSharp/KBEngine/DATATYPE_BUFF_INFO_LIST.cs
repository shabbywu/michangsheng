using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_BUFF_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_BUFF_INFO itemType = new DATATYPE_BUFF_INFO();

		public List<BUFF_INFO> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<BUFF_INFO> list = new List<BUFF_INFO>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<BUFF_INFO> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray();

	public BUFF_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new BUFF_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, BUFF_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
