using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C27 RID: 3111
	public class Message_Client_onEntityLeaveWorld : Message
	{
		// Token: 0x06005507 RID: 21767 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityLeaveWorld(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00235C60 File Offset: 0x00233E60
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityLeaveWorld(eid);
		}
	}
}
