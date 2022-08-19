using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B49 RID: 2889
	public class DATATYPE_AnonymousArray_43 : DATATYPE_BASE
	{
		// Token: 0x060050EC RID: 20716 RVA: 0x00221110 File Offset: 0x0021F310
		public List<int> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<int> list = new List<int>();
			while (num > 0U)
			{
				num -= 1U;
				list.Add(stream.readInt32());
			}
			return list;
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00221144 File Offset: 0x0021F344
		public void addToStreamEx(Bundle stream, List<int> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeInt32(v[i]);
			}
		}
	}
}
