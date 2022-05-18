using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAF RID: 4015
	public class Message_Client_onUpdatePropertys : Message
	{
		// Token: 0x06005F4B RID: 24395 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdatePropertys(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x00042941 File Offset: 0x00040B41
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdatePropertys(msgstream);
		}
	}
}
