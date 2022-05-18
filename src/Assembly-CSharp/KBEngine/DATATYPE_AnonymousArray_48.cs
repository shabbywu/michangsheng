using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000ECB RID: 3787
	public class DATATYPE_AnonymousArray_48 : DATATYPE_BASE
	{
		// Token: 0x06005B37 RID: 23351 RVA: 0x002509D8 File Offset: 0x0024EBD8
		public List<string> createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			List<string> list = new List<string>();
			while (num > 0U)
			{
				num -= 1U;
				list.Add(stream.readUnicode());
			}
			return list;
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x00250A0C File Offset: 0x0024EC0C
		public void addToStreamEx(Bundle stream, List<string> v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeUnicode(v[i]);
			}
		}
	}
}
