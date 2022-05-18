using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F72 RID: 3954
	public class Message_Client_onUpdateData_p : Message
	{
		// Token: 0x06005ED1 RID: 24273 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_p(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x0004269E File Offset: 0x0004089E
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_p(msgstream);
		}
	}
}
