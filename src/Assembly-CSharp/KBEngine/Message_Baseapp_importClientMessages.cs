using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCF RID: 4047
	public class Message_Baseapp_importClientMessages : Message
	{
		// Token: 0x06005F8B RID: 24459 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_importClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
