using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B46 RID: 2886
	public class DATATYPE_ITEM_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050E3 RID: 20707 RVA: 0x0022103E File Offset: 0x0021F23E
		public ITEM_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new ITEM_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x00221057 File Offset: 0x0021F257
		public void addToStreamEx(Bundle stream, ITEM_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F7F RID: 20351
		private DATATYPE_ITEM_INFO_LIST.DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_ITEM_INFO_LIST.DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015F1 RID: 5617
		public class DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x0600859C RID: 34204 RVA: 0x002E4A10 File Offset: 0x002E2C10
			public List<ITEM_INFO> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<ITEM_INFO> list = new List<ITEM_INFO>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x0600859D RID: 34205 RVA: 0x002E4A48 File Offset: 0x002E2C48
			public void addToStreamEx(Bundle stream, List<ITEM_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070E0 RID: 28896
			private DATATYPE_ITEM_INFO itemType = new DATATYPE_ITEM_INFO();
		}
	}
}
