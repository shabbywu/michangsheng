using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C3A RID: 3130
	public class Message_Loginapp_reqCreateMailAccount : Message
	{
		// Token: 0x0600552D RID: 21805 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_reqCreateMailAccount(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
