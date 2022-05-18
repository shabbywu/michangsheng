using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA5 RID: 4005
	public class Message_Client_onCreateAccountResult : Message
	{
		// Token: 0x06005F37 RID: 24375 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onCreateAccountResult(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x000428F3 File Offset: 0x00040AF3
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onCreateAccountResult(msgstream);
		}
	}
}
