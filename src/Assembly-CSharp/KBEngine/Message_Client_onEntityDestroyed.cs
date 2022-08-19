using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2B RID: 3115
	public class Message_Client_onEntityDestroyed : Message
	{
		// Token: 0x0600550F RID: 21775 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onEntityDestroyed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00235CB8 File Offset: 0x00233EB8
		public override void handleMessage(MemoryStream msgstream)
		{
			int eid = msgstream.readInt32();
			KBEngineApp.app.Client_onEntityDestroyed(eid);
		}
	}
}
