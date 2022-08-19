using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2E RID: 3118
	public class Message_Client_onStreamDataCompleted : Message
	{
		// Token: 0x06005515 RID: 21781 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onStreamDataCompleted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x00235D14 File Offset: 0x00233F14
		public override void handleMessage(MemoryStream msgstream)
		{
			short id = msgstream.readInt16();
			KBEngineApp.app.Client_onStreamDataCompleted(id);
		}
	}
}
