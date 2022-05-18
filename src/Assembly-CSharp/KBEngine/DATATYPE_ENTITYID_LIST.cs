using System;

namespace KBEngine
{
	// Token: 0x02000EB2 RID: 3762
	public class DATATYPE_ENTITYID_LIST : DATATYPE_BASE
	{
		// Token: 0x06005AEC RID: 23276 RVA: 0x002504AC File Offset: 0x0024E6AC
		public ENTITYID_LIST createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			ENTITYID_LIST entityid_LIST = new ENTITYID_LIST();
			while (num > 0U)
			{
				num -= 1U;
				entityid_LIST.Add(stream.readInt32());
			}
			return entityid_LIST;
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x002504E0 File Offset: 0x0024E6E0
		public void addToStreamEx(Bundle stream, ENTITYID_LIST v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeInt32(v[i]);
			}
		}
	}
}
