using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2D RID: 3117
	public class Message_Client_onStreamDataRecv : Message
	{
		// Token: 0x06005513 RID: 21779 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onStreamDataRecv(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x00235D07 File Offset: 0x00233F07
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onStreamDataRecv(msgstream);
		}
	}
}
