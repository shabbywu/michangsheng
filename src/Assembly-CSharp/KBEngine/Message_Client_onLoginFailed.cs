using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C22 RID: 3106
	public class Message_Client_onLoginFailed : Message
	{
		// Token: 0x060054FD RID: 21757 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onLoginFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00235BE6 File Offset: 0x00233DE6
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onLoginFailed(msgstream);
		}
	}
}
