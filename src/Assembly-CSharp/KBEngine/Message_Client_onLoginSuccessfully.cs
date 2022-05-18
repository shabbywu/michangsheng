using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA6 RID: 4006
	public class Message_Client_onLoginSuccessfully : Message
	{
		// Token: 0x06005F39 RID: 24377 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onLoginSuccessfully(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x00042900 File Offset: 0x00040B00
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onLoginSuccessfully(msgstream);
		}
	}
}
