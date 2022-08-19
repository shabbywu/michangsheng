using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4D RID: 2893
	public class DATATYPE_AnonymousArray_47 : DATATYPE_BASE
	{
		// Token: 0x060050F8 RID: 20728 RVA: 0x002212C0 File Offset: 0x0021F4C0
		public List<uint> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<uint> list = new List<uint>();
			while (num > 0U)
			{
				num -= 1U;
				list.Add(stream.readUint32());
			}
			return list;
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x002212F4 File Offset: 0x0021F4F4
		public void addToStreamEx(Bundle stream, List<uint> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeUint32(v[i]);
			}
		}
	}
}
