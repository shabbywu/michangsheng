using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCA RID: 4042
	public class Message_Baseapp_hello : Message
	{
		// Token: 0x06005F81 RID: 24449 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_hello(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
