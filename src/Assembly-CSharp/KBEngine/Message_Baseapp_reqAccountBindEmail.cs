using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C42 RID: 3138
	public class Message_Baseapp_reqAccountBindEmail : Message
	{
		// Token: 0x0600553D RID: 21821 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_reqAccountBindEmail(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
