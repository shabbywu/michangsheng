using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAD RID: 4013
	public class Message_Client_onEntityEnterSpace : Message
	{
		// Token: 0x06005F47 RID: 24391 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityEnterSpace(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x00042934 File Offset: 0x00040B34
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityEnterSpace(msgstream);
		}
	}
}
