using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1D RID: 3101
	public class Message_Client_onReqAccountNewPasswordCB : Message
	{
		// Token: 0x060054F3 RID: 21747 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onReqAccountNewPasswordCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x00235B94 File Offset: 0x00233D94
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReqAccountNewPasswordCB(failcode);
		}
	}
}
