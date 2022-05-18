using System;

namespace KBEngine
{
	// Token: 0x02000EC3 RID: 3779
	public class DATATYPE_CHECKIN_INFO : DATATYPE_BASE
	{
		// Token: 0x06005B1F RID: 23327 RVA: 0x000404D4 File Offset: 0x0003E6D4
		public CHECKIN_INFO createFromStreamEx(MemoryStream stream)
		{
			return new CHECKIN_INFO
			{
				type = stream.readUint16(),
				count = stream.readUint16(),
				time = stream.readUint32()
			};
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x000404FF File Offset: 0x0003E6FF
		public void addToStreamEx(Bundle stream, CHECKIN_INFO v)
		{
			stream.writeUint16(v.type);
			stream.writeUint16(v.count);
			stream.writeUint32(v.time);
		}
	}
}
