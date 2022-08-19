using System;

namespace KBEngine
{
	// Token: 0x02000B3C RID: 2876
	public class DATATYPE_AVATAR_INFO : DATATYPE_BASE
	{
		// Token: 0x060050C5 RID: 20677 RVA: 0x00220CDF File Offset: 0x0021EEDF
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

		// Token: 0x060050C6 RID: 20678 RVA: 0x00220D16 File Offset: 0x0021EF16
		public void addToStreamEx(Bundle stream, AVATAR_INFO v)
		{
			stream.writeUint64(v.dbid);
			stream.writeUnicode(v.name);
			stream.writeUint16(v.roleType);
			stream.writeUint32(v.level);
		}
	}
}
