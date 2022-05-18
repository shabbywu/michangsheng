using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA7 RID: 4007
	public class Message_Client_onLoginFailed : Message
	{
		// Token: 0x06005F3B RID: 24379 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onLoginFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x0004290D File Offset: 0x00040B0D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onLoginFailed(msgstream);
		}
	}
}
