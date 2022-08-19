using System;

namespace KBEngine
{
	// Token: 0x02000B3A RID: 2874
	public class DATATYPE_ENTITY_FORBID_COUNTER : DATATYPE_BASE
	{
		// Token: 0x060050BF RID: 20671 RVA: 0x00220C00 File Offset: 0x0021EE00
		public ENTITY_FORBID_COUNTER createFromStreamEx(MemoryStream stream)
		{
			uint num = stream.readUint32();
			ENTITY_FORBID_COUNTER entity_FORBID_COUNTER = new ENTITY_FORBID_COUNTER();
			while (num > 0U)
			{
				num -= 1U;
				entity_FORBID_COUNTER.Add(stream.readInt8());
			}
			return entity_FORBID_COUNTER;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00220C34 File Offset: 0x0021EE34
		public void addToStreamEx(Bundle stream, ENTITY_FORBID_COUNTER v)
		{
			stream.writeUint32((uint)v.Count);
			for (int i = 0; i < v.Count; i++)
			{
				stream.writeInt8(v[i]);
			}
		}
	}
}
