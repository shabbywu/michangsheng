using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C4C RID: 3148
	public class Message_Entity_onRemoteMethodCall : Message
	{
		// Token: 0x06005551 RID: 21841 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Entity_onRemoteMethodCall(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
