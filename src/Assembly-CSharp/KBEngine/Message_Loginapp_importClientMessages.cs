using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C39 RID: 3129
	public class Message_Loginapp_importClientMessages : Message
	{
		// Token: 0x0600552B RID: 21803 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_importClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
