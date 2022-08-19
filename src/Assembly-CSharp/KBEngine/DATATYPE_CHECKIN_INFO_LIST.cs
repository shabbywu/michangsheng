using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B48 RID: 2888
	public class DATATYPE_CHECKIN_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x002210CF File Offset: 0x0021F2CF
		public CHECKIN_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new CHECKIN_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x002210E8 File Offset: 0x0021F2E8
		public void addToStreamEx(Bundle stream, CHECKIN_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04004F80 RID: 20352
		private DATATYPE_CHECKIN_INFO_LIST.DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_CHECKIN_INFO_LIST.DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x020015F2 RID: 5618
		public class DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x0600859F RID: 34207 RVA: 0x002E4A98 File Offset: 0x002E2C98
			public List<CHECKIN_INFO> createFromStreamEx(MemoryStream stream)
			{
				uint num = stream.readUint32();
				List<CHECKIN_INFO> list = new List<CHECKIN_INFO>();
				while (num > 0U)
				{
					num -= 1U;
					list.Add(this.itemType.createFromStreamEx(stream));
				}
				return list;
			}

			// Token: 0x060085A0 RID: 34208 RVA: 0x002E4AD0 File Offset: 0x002E2CD0
			public void addToStreamEx(Bundle stream, List<CHECKIN_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x040070E1 RID: 28897
			private DATATYPE_CHECKIN_INFO itemType = new DATATYPE_CHECKIN_INFO();
		}
	}
}
