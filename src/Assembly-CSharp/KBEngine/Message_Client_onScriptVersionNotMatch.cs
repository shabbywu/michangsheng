using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB8 RID: 4024
	public class Message_Client_onScriptVersionNotMatch : Message
	{
		// Token: 0x06005F5D RID: 24413 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onScriptVersionNotMatch(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x00042982 File Offset: 0x00040B82
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onScriptVersionNotMatch(msgstream);
		}
	}
}
