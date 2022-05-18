using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB2 RID: 4018
	public class Message_Client_onStreamDataRecv : Message
	{
		// Token: 0x06005F51 RID: 24401 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onStreamDataRecv(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x0004294E File Offset: 0x00040B4E
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onStreamDataRecv(msgstream);
		}
	}
}
