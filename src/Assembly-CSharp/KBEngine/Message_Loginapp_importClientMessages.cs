using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FBE RID: 4030
	public class Message_Loginapp_importClientMessages : Message
	{
		// Token: 0x06005F69 RID: 24425 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_importClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
