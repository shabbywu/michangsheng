using System;

namespace KBEngine
{
	// Token: 0x02000B45 RID: 2885
	public class DATATYPE_ITEM_INFO : DATATYPE_BASE
	{
		// Token: 0x060050E0 RID: 20704 RVA: 0x00220FD5 File Offset: 0x0021F1D5
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

		// Token: 0x060050E1 RID: 20705 RVA: 0x0022100C File Offset: 0x0021F20C
		public void addToStreamEx(Bundle stream, ITEM_INFO v)
		{
			stream.writeUint64(v.UUID);
			stream.writeInt32(v.itemId);
			stream.writeUint32(v.itemCount);
			stream.writeInt32(v.itemIndex);
		}
	}
}
