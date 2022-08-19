using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B40 RID: 2880
	public class DATATYPE_SHOPBUY_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050D1 RID: 20689 RVA: 0x00220DF3 File Offset: 0x0021EFF3
		public SHOPBUY_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new SHOPBUY_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00220E0C File Offset: 0x0021F00C
		public void addToStreamEx(Bundle stream, SHOPBUY_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F7B RID: 20347
		private DATATYPE_SHOPBUY_INFO_LIST.DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_SHOPBUY_INFO_LIST.DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015ED RID: 5613
		public class DATATYPE__SHOPBUY_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06008590 RID: 34192 RVA: 0x002E47F0 File Offset: 0x002E29F0
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

			// Token: 0x06008591 RID: 34193 RVA: 0x002E4828 File Offset: 0x002E2A28
			public void addToStreamEx(Bundle stream, List<ShopBuyInfo> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070DC RID: 28892
			private DATATYPE_ShopBuyInfo itemType = new DATATYPE_ShopBuyInfo();
		}
	}
}
