using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FBF RID: 4031
	public class Message_Loginapp_reqCreateMailAccount : Message
	{
		// Token: 0x06005F6B RID: 24427 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_reqCreateMailAccount(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
