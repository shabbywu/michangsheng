using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAE RID: 4014
	public class Message_Client_onEntityLeaveSpace : Message
	{
		// Token: 0x06005F49 RID: 24393 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityLeaveSpace(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x00262ED8 File Offset: 0x002610D8
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityLeaveSpace(eid);
		}
	}
}
