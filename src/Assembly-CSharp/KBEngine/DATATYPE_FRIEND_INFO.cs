using System;

namespace KBEngine
{
	// Token: 0x02000EB4 RID: 3764
	public class DATATYPE_FRIEND_INFO : DATATYPE_BASE
	{
		// Token: 0x06005AF2 RID: 23282 RVA: 0x00040221 File Offset: 0x0003E421
		public FRIEND_INFO createFromStreamEx(MemoryStream stream)
		{
			return new FRIEND_INFO
			{
				dbid = stream.readUint64(),
				name = stream.readUnicode(),
				level = stream.readUint32()
			};
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0004024C File Offset: 0x0003E44C
		public void addToStreamEx(Bundle stream, FRIEND_INFO v)
		{
			stream.writeUint64(v.dbid);
			stream.writeUnicode(v.name);
			stream.writeUint32(v.level);
		}
	}
}
