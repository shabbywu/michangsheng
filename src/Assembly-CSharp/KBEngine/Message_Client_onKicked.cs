using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2F RID: 3119
	public class Message_Client_onKicked : Message
	{
		// Token: 0x06005517 RID: 21783 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onKicked(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x00235D34 File Offset: 0x00233F34
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onKicked(failedcode);
		}
	}
}
