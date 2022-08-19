using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE1 RID: 3041
	public class Message_Client_onRemoteMethodCallOptimized : Message
	{
		// Token: 0x0600547B RID: 21627 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onRemoteMethodCallOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x002357E4 File Offset: 0x002339E4
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onRemoteMethodCallOptimized(msgstream);
		}
	}
}
