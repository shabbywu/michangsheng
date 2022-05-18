using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EB7 RID: 3767
	public class DATATYPE_SHOPBUY_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005AFB RID: 23291 RVA: 0x000402CC File Offset: 0x0003E4CC
		public SHOPBUY_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new SHOPBUY_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x000402E5 File Offset: 0x0003E4E5
		public void addToStreamEx(Bundle stream, SHOPBUY_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A00 RID: 23040
		private DATATYPE_SHOPBUY_INFO_LIST.DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_SHOPBUY_INFO_LIST.DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EB8 RID: 3768
		public class DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005AFE RID: 23294 RVA: 0x00250518 File Offset: 0x0024E718
			public List<ShopBuyInfo> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<ShopBuyInfo> list = new List<ShopBuyInfo>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x06005AFF RID: 23295 RVA: 0x00250550 File Offset: 0x0024E750
			public void addToStreamEx(Bundle stream, List<ShopBuyInfo> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A01 RID: 23041
			private DATATYPE_ShopBuyInfo itemType = new DATATYPE_ShopBuyInfo();
		}
	}
}
