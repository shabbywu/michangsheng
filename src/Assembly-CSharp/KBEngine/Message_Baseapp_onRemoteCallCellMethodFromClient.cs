using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C48 RID: 3144
	public class Message_Baseapp_onRemoteCallCellMethodFromClient : Message
	{
		// Token: 0x06005549 RID: 21833 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_onRemoteCallCellMethodFromClient(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
