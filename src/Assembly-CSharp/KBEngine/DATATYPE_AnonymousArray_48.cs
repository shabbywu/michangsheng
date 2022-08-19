using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4E RID: 2894
	public class DATATYPE_AnonymousArray_48 : DATATYPE_BASE
	{
		// Token: 0x060050FB RID: 20731 RVA: 0x0022132C File Offset: 0x0021F52C
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

		// Token: 0x060050FC RID: 20732 RVA: 0x00221360 File Offset: 0x0021F560
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
