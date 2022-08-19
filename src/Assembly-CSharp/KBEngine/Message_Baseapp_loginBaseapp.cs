using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C46 RID: 3142
	public class Message_Baseapp_loginBaseapp : Message
	{
		// Token: 0x06005545 RID: 21829 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_loginBaseapp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
