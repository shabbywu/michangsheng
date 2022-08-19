using System;

namespace KBEngine
{
	// Token: 0x02000B43 RID: 2883
	public class DATATYPE_PLAYER_INFO : DATATYPE_BASE
	{
		// Token: 0x060050DA RID: 20698 RVA: 0x00220EB4 File Offset: 0x0021F0B4
		public PLAYER_INFO createFromStreamEx(MemoryStream stream)
		{
			return new PLAYER_INFO
			{
				dbid = stream.readUint64(),
				name = stream.readUnicode(),
				roleType = stream.readUint8(),
				level = stream.readUint16(),
				Exp = stream.readUint16(),
				jade = stream.readUint16(),
				gold = stream.readUint16(),
				soul = stream.readUint16()
			};
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00220F28 File Offset: 0x0021F128
		public void addToStreamEx(Bundle stream, PLAYER_INFO v)
		{
			stream.writeUint64(v.dbid);
			stream.writeUnicode(v.name);
			stream.writeUint8(v.roleType);
			stream.writeUint16(v.level);
			stream.writeUint16(v.Exp);
			stream.writeUint16(v.jade);
			stream.writeUint16(v.gold);
			stream.writeUint16(v.soul);
		}
	}
}
