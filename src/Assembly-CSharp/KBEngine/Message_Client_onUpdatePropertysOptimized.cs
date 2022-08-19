using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE2 RID: 3042
	public class Message_Client_onUpdatePropertysOptimized : Message
	{
		// Token: 0x0600547D RID: 21629 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdatePropertysOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x002357F1 File Offset: 0x002339F1
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdatePropertysOptimized(msgstream);
		}
	}
}
