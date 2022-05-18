using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC5 RID: 4037
	public class Message_Baseapp_onUpdateDataFromClient : Message
	{
		// Token: 0x06005F77 RID: 24439 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_onUpdateDataFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
