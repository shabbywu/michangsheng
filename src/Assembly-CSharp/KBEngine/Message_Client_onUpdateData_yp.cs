using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F6E RID: 3950
	public class Message_Client_onUpdateData_yp : Message
	{
		// Token: 0x06005EC9 RID: 24265 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ECA RID: 24266 RVA: 0x0004266A File Offset: 0x0004086A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yp(msgstream);
		}
	}
}
