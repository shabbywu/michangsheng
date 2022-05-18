using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FBA RID: 4026
	public class Message_Client_onControlEntity : Message
	{
		// Token: 0x06005F61 RID: 24417 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onControlEntity(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x00262F88 File Offset: 0x00261188
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			sbyte isControlled = msgstream.readInt8();
			KBEngineApp.app.Client_onControlEntity(eid, isControlled);
		}
	}
}
