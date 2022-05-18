using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB5 RID: 4021
	public class Message_Client_onImportClientMessages : Message
	{
		// Token: 0x06005F57 RID: 24407 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onImportClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0004295B File Offset: 0x00040B5B
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientMessages(msgstream);
		}
	}
}
