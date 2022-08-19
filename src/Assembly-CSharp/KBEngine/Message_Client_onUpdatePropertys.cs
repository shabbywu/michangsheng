using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2A RID: 3114
	public class Message_Client_onUpdatePropertys : Message
	{
		// Token: 0x0600550D RID: 21773 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdatePropertys(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00235CAB File Offset: 0x00233EAB
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdatePropertys(msgstream);
		}
	}
}
