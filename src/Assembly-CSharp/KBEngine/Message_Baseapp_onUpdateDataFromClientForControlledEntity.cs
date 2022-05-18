using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC6 RID: 4038
	public class Message_Baseapp_onUpdateDataFromClientForControlledEntity : Message
	{
		// Token: 0x06005F79 RID: 24441 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_onUpdateDataFromClientForControlledEntity(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
