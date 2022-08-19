using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C23 RID: 3107
	public class Message_Client_onCreatedProxies : Message
	{
		// Token: 0x060054FF RID: 21759 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onCreatedProxies(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00235BF4 File Offset: 0x00233DF4
		public override void handleMessage(MemoryStream msgstream)
		{
			ulong rndUUID = msgstream.readUint64();
			int eid = msgstream.readInt32();
			string entityType = msgstream.readString();
			KBEngineApp.app.Client_onCreatedProxies(rndUUID, eid, entityType);
		}
	}
}
