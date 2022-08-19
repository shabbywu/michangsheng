using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE7 RID: 3047
	public class Message_Client_onUpdateData : Message
	{
		// Token: 0x06005487 RID: 21639 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0023586F File Offset: 0x00233A6F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData(msgstream);
		}
	}
}
