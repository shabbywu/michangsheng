using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C44 RID: 3140
	public class Message_Entity_forwardEntityMessageToCellappFromClient : Message
	{
		// Token: 0x06005541 RID: 21825 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Entity_forwardEntityMessageToCellappFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
