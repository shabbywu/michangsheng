using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE0 RID: 3040
	public class Message_Client_onEntityLeaveWorldOptimized : Message
	{
		// Token: 0x06005479 RID: 21625 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityLeaveWorldOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x002357D7 File Offset: 0x002339D7
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityLeaveWorldOptimized(msgstream);
		}
	}
}
