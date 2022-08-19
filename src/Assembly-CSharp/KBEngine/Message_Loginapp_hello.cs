using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C38 RID: 3128
	public class Message_Loginapp_hello : Message
	{
		// Token: 0x06005529 RID: 21801 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_hello(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
