using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC7 RID: 4039
	public class Message_Baseapp_reqAccountBindEmail : Message
	{
		// Token: 0x06005F7B RID: 24443 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_reqAccountBindEmail(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
