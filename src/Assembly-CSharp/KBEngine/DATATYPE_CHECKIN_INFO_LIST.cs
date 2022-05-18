using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EC4 RID: 3780
	public class DATATYPE_CHECKIN_INFO_LIST : DATATYPE_BASE
	{
		// Token: 0x06005B22 RID: 23330 RVA: 0x00040525 File Offset: 0x0003E725
		public CHECKIN_INFO_LIST createFromStreamEx(MemoryStream stream)
		{
			return new CHECKIN_INFO_LIST
			{
				values = this.values_DataType.createFromStreamEx(stream)
			};
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0004053E File Offset: 0x0003E73E
		public void addToStreamEx(Bundle stream, CHECKIN_INFO_LIST v)
		{
			this.values_DataType.addToStreamEx(stream, v.values);
		}

		// Token: 0x04005A0A RID: 23050
		private DATATYPE_CHECKIN_INFO_LIST.DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray values_DataType = new DATATYPE_CHECKIN_INFO_LIST.DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray();

		// Token: 0x02000EC5 RID: 3781
		public class DATATYPE__CHECKIN_INFO_LIST_values_ArrayType_ChildArray : DATATYPE_BASE
		{
			// Token: 0x06005B25 RID: 23333 RVA: 0x00250854 File Offset: 0x0024EA54
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

			// Token: 0x06005B26 RID: 23334 RVA: 0x0025088C File Offset: 0x0024EA8C
			public void addToStreamEx(Bundle stream, List<CHECKIN_INFO> v)
			{
				stream.writeUint32((uint)v.Count);
				for (int i = 0; i < v.Count; i++)
				{
					this.itemType.addToStreamEx(stream, v[i]);
				}
			}

			// Token: 0x04005A0B RID: 23051
			private DATATYPE_CHECKIN_INFO itemType = new DATATYPE_CHECKIN_INFO();
		}
	}
}
