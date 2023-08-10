using System.Collections.Generic;

namespace KBEngine;

public class DATATYPE_SHOPBUY_INFO_LIST : DATATYPE_BASE
{
	public class DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
	{
		private DATATYPE_ShopBuyInfo itemType = new DATATYPE_ShopBuyInfo();

		public List<ShopBuyInfo> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<ShopBuyInfo> list = new List<ShopBuyInfo>();
			while (num != 0)
			{
				num--;
				list.Add(itemType.createFromStreamEx(stream));
			}
			return list;
		}

		public void addToStreamEx(Bundle stream, List<ShopBuyInfo> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				itemType.addToStreamEx(stream, v[i]);
			}
		}
	}

	private DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray();

	public SHOPBUY_INFO_LIST createFromStreamEx(MemoryStream stream)
	{
		return new SHOPBUY_INFO_LIST
		{
			values = values_DataType.createFromStreamEx(stream)
		};
	}

	public void addToStreamEx(Bundle stream, SHOPBUY_INFO_LIST v)
	{
		values_DataType.addToStreamEx(stream, v.values);
	}
}
