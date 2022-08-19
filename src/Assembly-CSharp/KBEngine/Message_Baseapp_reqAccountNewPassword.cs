using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C43 RID: 3139
	public class Message_Baseapp_reqAccountNewPassword : Message
	{
		// Token: 0x0600553F RID: 21823 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_reqAccountNewPassword(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
