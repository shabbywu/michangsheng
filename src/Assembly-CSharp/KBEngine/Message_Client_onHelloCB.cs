using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB7 RID: 4023
	public class Message_Client_onHelloCB : Message
	{
		// Token: 0x06005F5B RID: 24411 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onHelloCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x00042975 File Offset: 0x00040B75
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onHelloCB(msgstream);
		}
	}
}
