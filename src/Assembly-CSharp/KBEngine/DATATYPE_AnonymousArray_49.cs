using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4F RID: 2895
	public class DATATYPE_AnonymousArray_49 : DATATYPE_BASE
	{
		// Token: 0x060050FE RID: 20734 RVA: 0x00221398 File Offset: 0x0021F598
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

		// Token: 0x060050FF RID: 20735 RVA: 0x002213CC File Offset: 0x0021F5CC
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
