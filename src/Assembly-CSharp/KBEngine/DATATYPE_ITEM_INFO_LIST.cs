using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EC1 RID: 3777
	public class DATATYPE_ITEM_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005B19 RID: 23321 RVA: 0x00040481 File Offset: 0x0003E681
		public ITEM_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new ITEM_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x0004049A File Offset: 0x0003E69A
		public void addToStreamEx(Bundle stream, ITEM_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A08 RID: 23048
		private DATATYPE_ITEM_INFO_LIST.DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_ITEM_INFO_LIST.DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EC2 RID: 3778
		public class DATATYPE__ITEM_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005B1C RID: 23324 RVA: 0x002507DC File Offset: 0x0024E9DC
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

			// Token: 0x06005B1D RID: 23325 RVA: 0x00250814 File Offset: 0x0024EA14
			public void addToStreamEx(Bundle stream, List<ITEM_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A09 RID: 23049
			private DATATYPE_ITEM_INFO itemType = new DATATYPE_ITEM_INFO();
		}
	}
}
