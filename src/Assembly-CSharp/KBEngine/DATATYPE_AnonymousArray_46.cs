using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4C RID: 2892
	public class DATATYPE_AnonymousArray_46 : DATATYPE_BASE
	{
		// Token: 0x060050F5 RID: 20725 RVA: 0x00221254 File Offset: 0x0021F454
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

		// Token: 0x060050F6 RID: 20726 RVA: 0x00221288 File Offset: 0x0021F488
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
