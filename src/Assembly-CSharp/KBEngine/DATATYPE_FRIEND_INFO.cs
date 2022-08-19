using System;

namespace KBEngine
{
	// Token: 0x02000B3D RID: 2877
	public class DATATYPE_FRIEND_INFO : DATATYPE_BASE
	{
		// Token: 0x060050C8 RID: 20680 RVA: 0x00220D48 File Offset: 0x0021EF48
		public FRIEND_INFO createFromStreamEx(MemoryStream stream)
		{
			return new FRIEND_INFO
			{
				dbid = stream.readUint64(),
				name = stream.readUnicode(),
				level = stream.readUint32()
			};
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00220D73 File Offset: 0x0021EF73
		public void addToStreamEx(Bundle stream, FRIEND_INFO v)
		{
			stream.writeUint64(v.dbid);
			stream.writeUnicode(v.name);
			stream.writeUint32(v.level);
		}
	}
}
