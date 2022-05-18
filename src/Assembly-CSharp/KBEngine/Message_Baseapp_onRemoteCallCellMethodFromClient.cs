using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCD RID: 4045
	public class Message_Baseapp_onRemoteCallCellMethodFromClient : Message
	{
		// Token: 0x06005F87 RID: 24455 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_onRemoteCallCellMethodFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
