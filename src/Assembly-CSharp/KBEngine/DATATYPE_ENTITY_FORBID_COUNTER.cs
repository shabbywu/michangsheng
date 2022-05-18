using System;

namespace KBEngine
{
	// Token: 0x02000EB1 RID: 3761
	public class DATATYPE_ENTITY_FORBID_COUNTER : DATATYPE_BASE
	{
		// Token: 0x06005AE9 RID: 23273 RVA: 0x00250440 File Offset: 0x0024E640
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

		// Token: 0x06005AEA RID: 23274 RVA: 0x00250474 File Offset: 0x0024E674
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
