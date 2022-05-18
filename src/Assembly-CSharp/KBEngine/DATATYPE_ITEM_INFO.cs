using System;

namespace KBEngine
{
	// Token: 0x02000EC0 RID: 3776
	public class DATATYPE_ITEM_INFO : DATATYPE_BASE
	{
		// Token: 0x06005B16 RID: 23318 RVA: 0x00040418 File Offset: 0x0003E618
		public ITEM_INFO createFromStreamEx(MemoryStream stream)
		{
			return new ITEM_INFO
			{
				UUID = stream.readUint64(),
				itemId = stream.readInt32(),
				itemCount = stream.readUint32(),
				itemIndex = stream.readInt32()
			};
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x0004044F File Offset: 0x0003E64F
		public void addToStreamEx(Bundle stream, ITEM_INFO v)
		{
			stream.writeUint64(v.UUID);
			stream.writeInt32(v.itemId);
			stream.writeUint32(v.itemCount);
			stream.writeInt32(v.itemIndex);
		}
	}
}
