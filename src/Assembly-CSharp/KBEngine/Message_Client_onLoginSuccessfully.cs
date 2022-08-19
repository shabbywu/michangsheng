using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C21 RID: 3105
	public class Message_Client_onLoginSuccessfully : Message
	{
		// Token: 0x060054FB RID: 21755 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onLoginSuccessfully(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00235BD9 File Offset: 0x00233DD9
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onLoginSuccessfully(msgstream);
		}
	}
}
