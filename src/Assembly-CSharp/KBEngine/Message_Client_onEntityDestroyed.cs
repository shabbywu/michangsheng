using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB0 RID: 4016
	public class Message_Client_onEntityDestroyed : Message
	{
		// Token: 0x06005F4D RID: 24397 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityDestroyed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00262EF8 File Offset: 0x002610F8
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityDestroyed(eid);
		}
	}
}
