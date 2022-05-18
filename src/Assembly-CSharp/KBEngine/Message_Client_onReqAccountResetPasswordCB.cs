using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA0 RID: 4000
	public class Message_Client_onReqAccountResetPasswordCB : Message
	{
		// Token: 0x06005F2D RID: 24365 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onReqAccountResetPasswordCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x00262E08 File Offset: 0x00261008
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountResetPasswordCB(failcode);
		}
	}
}
