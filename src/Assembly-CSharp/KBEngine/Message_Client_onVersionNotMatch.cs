using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB9 RID: 4025
	public class Message_Client_onVersionNotMatch : Message
	{
		// Token: 0x06005F5F RID: 24415 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onVersionNotMatch(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x0004298F File Offset: 0x00040B8F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onVersionNotMatch(msgstream);
		}
	}
}
