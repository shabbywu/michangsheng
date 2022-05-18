using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA2 RID: 4002
	public class Message_Client_onReqAccountNewPasswordCB : Message
	{
		// Token: 0x06005F31 RID: 24369 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onReqAccountNewPasswordCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x00262E48 File Offset: 0x00261048
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountNewPasswordCB(failcode);
		}
	}
}
