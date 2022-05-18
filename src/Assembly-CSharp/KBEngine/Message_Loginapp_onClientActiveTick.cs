using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC2 RID: 4034
	public class Message_Loginapp_onClientActiveTick : Message
	{
		// Token: 0x06005F71 RID: 24433 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_onClientActiveTick(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
