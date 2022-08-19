using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C30 RID: 3120
	public class Message_Client_onImportClientMessages : Message
	{
		// Token: 0x06005519 RID: 21785 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onImportClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x00235D53 File Offset: 0x00233F53
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientMessages(msgstream);
		}
	}
}
