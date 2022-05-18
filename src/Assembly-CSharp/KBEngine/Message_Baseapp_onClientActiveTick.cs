using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCE RID: 4046
	public class Message_Baseapp_onClientActiveTick : Message
	{
		// Token: 0x06005F89 RID: 24457 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_onClientActiveTick(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
