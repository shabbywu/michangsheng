using System;

namespace KBEngine
{
	// Token: 0x02000B47 RID: 2887
	public class DATATYPE_CHECKIN_INFO : DATATYPE_BASE
	{
		// Token: 0x060050E6 RID: 20710 RVA: 0x0022107E File Offset: 0x0021F27E
		public CHECKIN_INFO createFromStreamEx(MemoryStream stream)
		{
			return new CHECKIN_INFO
			{
				type = stream.readUint16(),
				count = stream.readUint16(),
				time = stream.readUint32()
			};
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x002210A9 File Offset: 0x0021F2A9
		public void addToStreamEx(Bundle stream, CHECKIN_INFO v)
		{
			stream.writeUint16(v.type);
			stream.writeUint16(v.count);
			stream.writeUint32(v.time);
		}
	}
}
