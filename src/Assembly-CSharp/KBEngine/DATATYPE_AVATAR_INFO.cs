using System;

namespace KBEngine
{
	// Token: 0x02000EB3 RID: 3763
	public class DATATYPE_AVATAR_INFO : DATATYPE_BASE
	{
		// Token: 0x06005AEF RID: 23279 RVA: 0x000401B8 File Offset: 0x0003E3B8
		public AVATAR_INFO createFromStreamEx(MemoryStream stream)
		{
			return new AVATAR_INFO
			{
				dbid = stream.readUint64(),
				name = stream.readUnicode(),
				roleType = stream.readUint16(),
				level = stream.readUint32()
			};
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x000401EF File Offset: 0x0003E3EF
		public void addToStreamEx(Bundle stream, AVATAR_INFO v)
		{
			stream.writeUint64(v.dbid);
			stream.writeUnicode(v.name);
			stream.writeUint16(v.roleType);
			stream.writeUint32(v.level);
		}
	}
}
