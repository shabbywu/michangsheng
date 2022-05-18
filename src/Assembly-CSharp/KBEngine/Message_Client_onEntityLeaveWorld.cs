using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAC RID: 4012
	public class Message_Client_onEntityLeaveWorld : Message
	{
		// Token: 0x06005F45 RID: 24389 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityLeaveWorld(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00262EB8 File Offset: 0x002610B8
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityLeaveWorld(eid);
		}
	}
}
