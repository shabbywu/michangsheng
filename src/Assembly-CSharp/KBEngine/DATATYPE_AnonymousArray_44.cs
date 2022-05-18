using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000EC7 RID: 3783
	public class DATATYPE_AnonymousArray_44 : DATATYPE_BASE
	{
		// Token: 0x06005B2B RID: 23339 RVA: 0x002508CC File Offset: 0x0024EACC
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

		// Token: 0x06005B2C RID: 23340 RVA: 0x002504E0 File Offset: 0x0024E6E0
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
