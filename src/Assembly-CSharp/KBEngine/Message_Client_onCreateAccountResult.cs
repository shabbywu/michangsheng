using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C20 RID: 3104
	public class Message_Client_onCreateAccountResult : Message
	{
		// Token: 0x060054F9 RID: 21753 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onCreateAccountResult(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x00235BCC File Offset: 0x00233DCC
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onCreateAccountResult(msgstream);
		}
	}
}
