using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B42 RID: 2882
	public class DATATYPE_FRIEND_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050D7 RID: 20695 RVA: 0x00220E73 File Offset: 0x0021F073
		public FRIEND_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new FRIEND_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00220E8C File Offset: 0x0021F08C
		public void addToStreamEx(Bundle stream, FRIEND_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F7D RID: 20349
		private DATATYPE_FRIEND_INFO_LIST.DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_FRIEND_INFO_LIST.DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015EF RID: 5615
		public class DATATYPE__FRIEND_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06008596 RID: 34198 RVA: 0x002E4900 File Offset: 0x002E2B00
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

			// Token: 0x06008597 RID: 34199 RVA: 0x002E4938 File Offset: 0x002E2B38
			public void addToStreamEx(Bundle stream, List<FRIEND_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070DE RID: 28894
			private DATATYPE_FRIEND_INFO itemType = new DATATYPE_FRIEND_INFO();
		}
	}
}
