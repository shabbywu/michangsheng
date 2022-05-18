using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB4 RID: 4020
	public class Message_Client_onKicked : Message
	{
		// Token: 0x06005F55 RID: 24405 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onKicked(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x00262F68 File Offset: 0x00261168
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onKicked(failedcode);
		}
	}
}
