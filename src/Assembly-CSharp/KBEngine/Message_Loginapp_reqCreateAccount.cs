using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C36 RID: 3126
	public class Message_Loginapp_reqCreateAccount : Message
	{
		// Token: 0x06005525 RID: 21797 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_reqCreateAccount(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
