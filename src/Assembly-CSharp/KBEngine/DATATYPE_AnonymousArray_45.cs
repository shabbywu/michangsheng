using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000B4B RID: 2891
	public class DATATYPE_AnonymousArray_45 : DATATYPE_BASE
	{
		// Token: 0x060050F2 RID: 20722 RVA: 0x002211E8 File Offset: 0x0021F3E8
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

		// Token: 0x060050F3 RID: 20723 RVA: 0x0022121C File Offset: 0x0021F41C
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
