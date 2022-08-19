using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1C RID: 3100
	public class Message_Client_onReqAccountBindEmailCB : Message
	{
		// Token: 0x060054F1 RID: 21745 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onReqAccountBindEmailCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00235B74 File Offset: 0x00233D74
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountBindEmailCB(failcode);
		}
	}
}
