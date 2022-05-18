using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EB9 RID: 3769
	public class DATATYPE_BUFF_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005B01 RID: 23297 RVA: 0x0004031F File Offset: 0x0003E51F
		public BUFF_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new BUFF_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x00040338 File Offset: 0x0003E538
		public void addToStreamEx(Bundle stream, BUFF_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A02 RID: 23042
		private DATATYPE_BUFF_INFO_LIST.DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_BUFF_INFO_LIST.DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EBA RID: 3770
		public class DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005B04 RID: 23300 RVA: 0x00250590 File Offset: 0x0024E790
			public List<BUFF_INFO> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<BUFF_INFO> list = new List<BUFF_INFO>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x06005B05 RID: 23301 RVA: 0x002505C8 File Offset: 0x0024E7C8
			public void addToStreamEx(Bundle stream, List<BUFF_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A03 RID: 23043
			private DATATYPE_BUFF_INFO itemType = new DATATYPE_BUFF_INFO();
		}
	}
}
