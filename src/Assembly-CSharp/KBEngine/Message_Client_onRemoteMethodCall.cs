using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAA RID: 4010
	public class Message_Client_onRemoteMethodCall : Message
	{
		// Token: 0x06005F41 RID: 24385 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onRemoteMethodCall(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x0004291A File Offset: 0x00040B1A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onRemoteMethodCall(msgstream);
		}
	}
}
