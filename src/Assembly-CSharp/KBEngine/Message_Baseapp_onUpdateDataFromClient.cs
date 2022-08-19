using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C40 RID: 3136
	public class Message_Baseapp_onUpdateDataFromClient : Message
	{
		// Token: 0x06005539 RID: 21817 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_onUpdateDataFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
