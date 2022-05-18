using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA8 RID: 4008
	public class Message_Client_onCreatedProxies : Message
	{
		// Token: 0x06005F3D RID: 24381 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onCreatedProxies(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x00262E68 File Offset: 0x00261068
		public override void handleMessage(MemoryStream msgstream)
		{
			ulong rndUUID = msgstream.readUint64();
			int eid = msgstream.readInt32();
			string entityType = msgstream.readString();
			KBEngineApp.app.Client_onCreatedProxies(rndUUID, eid, entityType);
		}
	}
}
