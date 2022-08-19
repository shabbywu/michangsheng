using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C33 RID: 3123
	public class Message_Client_onScriptVersionNotMatch : Message
	{
		// Token: 0x0600551F RID: 21791 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onScriptVersionNotMatch(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00235D7A File Offset: 0x00233F7A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onScriptVersionNotMatch(msgstream);
		}
	}
}
