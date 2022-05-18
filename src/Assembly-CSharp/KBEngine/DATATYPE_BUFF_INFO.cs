using System;

namespace KBEngine
{
	// Token: 0x02000EB5 RID: 3765
	public class DATATYPE_BUFF_INFO : DATATYPE_BASE
	{
		// Token: 0x06005AF5 RID: 23285 RVA: 0x00040272 File Offset: 0x0003E472
		public BUFF_INFO createFromStreamEx(MemoryStream stream)
		{
			return new BUFF_INFO
			{
				buffid = stream.readUint32()
			};
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x00040285 File Offset: 0x0003E485
		public void addToStreamEx(Bundle stream, BUFF_INFO v)
		{
			stream.writeUint32(v.buffid);
		}
	}
}
