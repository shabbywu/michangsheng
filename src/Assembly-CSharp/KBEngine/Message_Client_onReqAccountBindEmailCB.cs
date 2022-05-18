using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA1 RID: 4001
	public class Message_Client_onReqAccountBindEmailCB : Message
	{
		// Token: 0x06005F2F RID: 24367 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onReqAccountBindEmailCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00262E28 File Offset: 0x00261028
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountBindEmailCB(failcode);
		}
	}
}
