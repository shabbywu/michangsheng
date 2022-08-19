using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C25 RID: 3109
	public class Message_Client_onRemoteMethodCall : Message
	{
		// Token: 0x06005503 RID: 21763 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onRemoteMethodCall(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x00235C43 File Offset: 0x00233E43
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onRemoteMethodCall(msgstream);
		}
	}
}
