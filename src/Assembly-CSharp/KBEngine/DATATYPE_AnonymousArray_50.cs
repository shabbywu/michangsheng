using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B50 RID: 2896
	public class DATATYPE_AnonymousArray_50 : DATATYPE_BASE
	{
		// Token: 0x06005101 RID: 20737 RVA: 0x00221404 File Offset: 0x0021F604
		public List<ushort> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<ushort> list = new List<ushort>();
			while (num > 0U)
			{
				num -= 1U;
				list.Add(stream.readUint16());
			}
			return list;
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00221438 File Offset: 0x0021F638
		public void addToStreamEx(Bundle stream, List<ushort> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeUint16(v[i]);
			}
		}
	}
}
