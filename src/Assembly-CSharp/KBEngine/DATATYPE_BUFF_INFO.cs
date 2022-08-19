using System;

namespace KBEngine
{
	// Token: 0x02000B3E RID: 2878
	public class DATATYPE_BUFF_INFO : DATATYPE_BASE
	{
		// Token: 0x060050CB RID: 20683 RVA: 0x00220D99 File Offset: 0x0021EF99
		public BUFF_INFO createFromStreamEx(MemoryStream stream)
		{
			return new BUFF_INFO
			{
				buffid = stream.readUint32()
			};
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00220DAC File Offset: 0x0021EFAC
		public void addToStreamEx(Bundle stream, BUFF_INFO v)
		{
			stream.writeUint32(v.buffid);
		}
	}
}
