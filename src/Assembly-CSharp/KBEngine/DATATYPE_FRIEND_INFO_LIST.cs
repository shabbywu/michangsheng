using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EBB RID: 3771
	public class DATATYPE_FRIEND_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005B07 RID: 23303 RVA: 0x00040372 File Offset: 0x0003E572
		public FRIEND_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new FRIEND_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x0004038B File Offset: 0x0003E58B
		public void addToStreamEx(Bundle stream, FRIEND_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A04 RID: 23044
		private DATATYPE_FRIEND_INFO_LIST.DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_FRIEND_INFO_LIST.DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EBC RID: 3772
		public class DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005B0A RID: 23306 RVA: 0x00250608 File Offset: 0x0024E808
			public List<FRIEND_INFO> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<FRIEND_INFO> list = new List<FRIEND_INFO>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x06005B0B RID: 23307 RVA: 0x00250640 File Offset: 0x0024E840
			public void addToStreamEx(Bundle stream, List<FRIEND_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A05 RID: 23045
			private DATATYPE_FRIEND_INFO itemType = new DATATYPE_FRIEND_INFO();
		}
	}
}
