using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FD0 RID: 4048
	public class Message_Baseapp_importClientEntityDef : Message
	{
		// Token: 0x06005F8D RID: 24461 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_importClientEntityDef(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
