using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000ECC RID: 3788
	public class DATATYPE_AnonymousArray_49 : DATATYPE_BASE
	{
		// Token: 0x06005B3A RID: 23354 RVA: 0x00250900 File Offset: 0x0024EB00
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

		// Token: 0x06005B3B RID: 23355 RVA: 0x00250934 File Offset: 0x0024EB34
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
