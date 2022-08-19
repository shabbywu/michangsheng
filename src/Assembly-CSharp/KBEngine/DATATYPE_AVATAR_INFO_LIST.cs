using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B44 RID: 2884
	public class DATATYPE_AVATAR_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050DD RID: 20701 RVA: 0x00220F95 File Offset: 0x0021F195
		public AVATAR_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new AVATAR_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x00220FAE File Offset: 0x0021F1AE
		public void addToStreamEx(Bundle stream, AVATAR_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F7E RID: 20350
		private DATATYPE_AVATAR_INFO_LIST.DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_AVATAR_INFO_LIST.DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015F0 RID: 5616
		public class DATATYPE__AVATAR_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06008599 RID: 34201 RVA: 0x002E4988 File Offset: 0x002E2B88
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

			// Token: 0x0600859A RID: 34202 RVA: 0x002E49C0 File Offset: 0x002E2BC0
			public void addToStreamEx(Bundle stream, List<AVATAR_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070DF RID: 28895
			private DATATYPE_AVATAR_INFO itemType = new DATATYPE_AVATAR_INFO();
		}
	}
}
