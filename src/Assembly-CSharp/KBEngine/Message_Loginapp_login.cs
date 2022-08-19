using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C37 RID: 3127
	public class Message_Loginapp_login : Message
	{
		// Token: 0x06005527 RID: 21799 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_login(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
