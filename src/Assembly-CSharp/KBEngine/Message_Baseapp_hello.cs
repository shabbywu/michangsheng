using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C45 RID: 3141
	public class Message_Baseapp_hello : Message
	{
		// Token: 0x06005543 RID: 21827 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_hello(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
