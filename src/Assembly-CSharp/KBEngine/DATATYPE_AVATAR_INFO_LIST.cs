using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EBE RID: 3774
	public class DATATYPE_AVATAR_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005B10 RID: 23312 RVA: 0x000403C5 File Offset: 0x0003E5C5
		public AVATAR_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new AVATAR_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x000403DE File Offset: 0x0003E5DE
		public void addToStreamEx(Bundle stream, AVATAR_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A06 RID: 23046
		private DATATYPE_AVATAR_INFO_LIST.DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_AVATAR_INFO_LIST.DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EBF RID: 3775
		public class DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005B13 RID: 23315 RVA: 0x00250764 File Offset: 0x0024E964
			public List<AVATAR_INFO> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<AVATAR_INFO> list = new List<AVATAR_INFO>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x06005B14 RID: 23316 RVA: 0x0025079C File Offset: 0x0024E99C
			public void addToStreamEx(Bundle stream, List<AVATAR_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A07 RID: 23047
			private DATATYPE_AVATAR_INFO itemType = new DATATYPE_AVATAR_INFO();
		}
	}
}
