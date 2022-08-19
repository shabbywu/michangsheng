using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B41 RID: 2881
	public class DATATYPE_BUFF_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050D4 RID: 20692 RVA: 0x00220E33 File Offset: 0x0021F033
		public BUFF_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new BUFF_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x00220E4C File Offset: 0x0021F04C
		public void addToStreamEx(Bundle stream, BUFF_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F7C RID: 20348
		private DATATYPE_BUFF_INFO_LIST.DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_BUFF_INFO_LIST.DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015EE RID: 5614
		public class DATATYPE__BUFF_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06008593 RID: 34195 RVA: 0x002E4878 File Offset: 0x002E2A78
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

			// Token: 0x06008594 RID: 34196 RVA: 0x002E48B0 File Offset: 0x002E2AB0
			public void addToStreamEx(Bundle stream, List<BUFF_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070DD RID: 28893
			private DATATYPE_BUFF_INFO itemType = new DATATYPE_BUFF_INFO();
		}
	}
}
