using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FD1 RID: 4049
	public class Message_Entity_onRemoteMethodCall : Message
	{
		// Token: 0x06005F8F RID: 24463 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Entity_onRemoteMethodCall(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
