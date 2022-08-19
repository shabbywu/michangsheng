using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C34 RID: 3124
	public class Message_Client_onVersionNotMatch : Message
	{
		// Token: 0x06005521 RID: 21793 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onVersionNotMatch(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x00235D87 File Offset: 0x00233F87
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onVersionNotMatch(msgstream);
		}
	}
}
