using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C29 RID: 3113
	public class Message_Client_onEntityLeaveSpace : Message
	{
		// Token: 0x0600550B RID: 21771 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityLeaveSpace(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x00235C8C File Offset: 0x00233E8C
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityLeaveSpace(eid);
		}
	}
}
