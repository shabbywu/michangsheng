using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C49 RID: 3145
	public class Message_Baseapp_onClientActiveTick : Message
	{
		// Token: 0x0600554B RID: 21835 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_onClientActiveTick(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
