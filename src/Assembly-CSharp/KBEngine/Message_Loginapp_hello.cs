using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FBD RID: 4029
	public class Message_Loginapp_hello : Message
	{
		// Token: 0x06005F67 RID: 24423 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_hello(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
