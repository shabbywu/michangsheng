using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C47 RID: 3143
	public class Message_Baseapp_reloginBaseapp : Message
	{
		// Token: 0x06005547 RID: 21831 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_reloginBaseapp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
