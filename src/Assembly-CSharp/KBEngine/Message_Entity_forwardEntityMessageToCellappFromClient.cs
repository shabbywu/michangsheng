using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC9 RID: 4041
	public class Message_Entity_forwardEntityMessageToCellappFromClient : Message
	{
		// Token: 0x06005F7F RID: 24447 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Entity_forwardEntityMessageToCellappFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
