using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1B RID: 3099
	public class Message_Client_onReqAccountResetPasswordCB : Message
	{
		// Token: 0x060054EF RID: 21743 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onReqAccountResetPasswordCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00235B54 File Offset: 0x00233D54
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountResetPasswordCB(failcode);
		}
	}
}
