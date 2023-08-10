using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_CHECKIN_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_CHECKIN_INFO itemType = new DATATYPE_CHECKIN_INFO();

		public List<CHECKIN_INFO> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<CHECKIN_INFO> list = new List<CHECKIN_INFO>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<CHECKIN_INFO> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray();

	public CHECKIN_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new CHECKIN_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, CHECKIN_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
