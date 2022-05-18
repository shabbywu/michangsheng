using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F65 RID: 3941
	public class Message_Client_onEntityLeaveWorldOptimized : Message
	{
		// Token: 0x06005EB7 RID: 24247 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityLeaveWorldOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0004260F File Offset: 0x0004080F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityLeaveWorldOptimized(msgstream);
		}
	}
}
