using System;

namespace KBEngine
{
	// Token: 0x02000EBD RID: 3773
	public class DATATYPE_PLAYER_INFO : DATATYPE_BASE
	{
		// Token: 0x06005B0D RID: 23309 RVA: 0x00250680 File Offset: 0x0024E880
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

		// Token: 0x06005B0E RID: 23310 RVA: 0x002506F4 File Offset: 0x0024E8F4
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
