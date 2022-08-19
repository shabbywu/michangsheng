using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C26 RID: 3110
	public class Message_Client_onEntityEnterWorld : Message
	{
		// Token: 0x06005505 RID: 21765 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityEnterWorld(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00235C50 File Offset: 0x00233E50
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityEnterWorld(msgstream);
		}
	}
}
