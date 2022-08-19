using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BEB RID: 3051
	public class Message_Client_onUpdateData_pr : Message
	{
		// Token: 0x0600548F RID: 21647 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x002358A3 File Offset: 0x00233AA3
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_pr(msgstream);
		}
	}
}
