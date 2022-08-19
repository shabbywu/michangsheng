using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C32 RID: 3122
	public class Message_Client_onHelloCB : Message
	{
		// Token: 0x0600551D RID: 21789 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onHelloCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00235D6D File Offset: 0x00233F6D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onHelloCB(msgstream);
		}
	}
}
