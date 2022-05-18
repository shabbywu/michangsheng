using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000ECD RID: 3789
	public class DATATYPE_AnonymousArray_50 : DATATYPE_BASE
	{
		// Token: 0x06005B3D RID: 23357 RVA: 0x00250900 File Offset: 0x0024EB00
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

		// Token: 0x06005B3E RID: 23358 RVA: 0x00250934 File Offset: 0x0024EB34
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
