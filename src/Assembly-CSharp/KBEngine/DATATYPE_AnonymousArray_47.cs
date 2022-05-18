using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000ECA RID: 3786
	public class DATATYPE_AnonymousArray_47 : DATATYPE_BASE
	{
		// Token: 0x06005B34 RID: 23348 RVA: 0x0025096C File Offset: 0x0024EB6C
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

		// Token: 0x06005B35 RID: 23349 RVA: 0x002509A0 File Offset: 0x0024EBA0
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
