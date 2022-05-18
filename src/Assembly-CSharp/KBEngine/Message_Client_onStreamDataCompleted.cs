using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB3 RID: 4019
	public class Message_Client_onStreamDataCompleted : Message
	{
		// Token: 0x06005F53 RID: 24403 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onStreamDataCompleted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x00262F48 File Offset: 0x00261148
		public override void handleMessage(MemoryStream msgstream)
		{
			short id = msgstream.readInt16();
			KBEngineApp.app.Client_onStreamDataCompleted(id);
		}
	}
}
