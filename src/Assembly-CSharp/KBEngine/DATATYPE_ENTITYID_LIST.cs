using System;

namespace KBEngine
{
	// Token: 0x02000B3B RID: 2875
	public class DATATYPE_ENTITYID_LIST : DATATYPE_BASE
	{
		// Token: 0x060050C2 RID: 20674 RVA: 0x00220C74 File Offset: 0x0021EE74
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

		// Token: 0x060050C3 RID: 20675 RVA: 0x00220CA8 File Offset: 0x0021EEA8
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
