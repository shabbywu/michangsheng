using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F6D RID: 3949
	public class Message_Client_onUpdateData_ypr : Message
	{
		// Token: 0x06005EC7 RID: 24263 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x0004265D File Offset: 0x0004085D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_ypr(msgstream);
		}
	}
}
