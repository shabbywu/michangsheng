using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C3E RID: 3134
	public class Message_Loginapp_reqAccountResetPassword : Message
	{
		// Token: 0x06005535 RID: 21813 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_reqAccountResetPassword(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
