using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE3 RID: 3043
	public class Message_Client_onSetEntityPosAndDir : Message
	{
		// Token: 0x0600547F RID: 21631 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onSetEntityPosAndDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x002357FE File Offset: 0x002339FE
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onSetEntityPosAndDir(msgstream);
		}
	}
}
