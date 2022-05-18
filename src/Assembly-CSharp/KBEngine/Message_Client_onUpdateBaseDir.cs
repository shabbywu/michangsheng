using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F6A RID: 3946
	public class Message_Client_onUpdateBaseDir : Message
	{
		// Token: 0x06005EC1 RID: 24257 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateBaseDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x00042643 File Offset: 0x00040843
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateBaseDir(msgstream);
		}
	}
}
