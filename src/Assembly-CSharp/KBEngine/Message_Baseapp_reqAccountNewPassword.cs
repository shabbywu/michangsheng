using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC8 RID: 4040
	public class Message_Baseapp_reqAccountNewPassword : Message
	{
		// Token: 0x06005F7D RID: 24445 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_reqAccountNewPassword(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
