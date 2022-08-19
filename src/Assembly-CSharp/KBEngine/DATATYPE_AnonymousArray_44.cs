using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4A RID: 2890
	public class DATATYPE_AnonymousArray_44 : DATATYPE_BASE
	{
		// Token: 0x060050EF RID: 20719 RVA: 0x0022117C File Offset: 0x0021F37C
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

		// Token: 0x060050F0 RID: 20720 RVA: 0x002211B0 File Offset: 0x0021F3B0
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
