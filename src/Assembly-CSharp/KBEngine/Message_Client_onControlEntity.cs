using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C35 RID: 3125
	public class Message_Client_onControlEntity : Message
	{
		// Token: 0x06005523 RID: 21795 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onControlEntity(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x00235D94 File Offset: 0x00233F94
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			sbyte isControlled = msgstream.readInt8();
			KBEngineApp.app.Client_onControlEntity(eid, isControlled);
		}
	}
}
