using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C28 RID: 3112
	public class Message_Client_onEntityEnterSpace : Message
	{
		// Token: 0x06005509 RID: 21769 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityEnterSpace(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00235C7F File Offset: 0x00233E7F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityEnterSpace(msgstream);
		}
	}
}
