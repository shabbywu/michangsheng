using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FBB RID: 4027
	public class Message_Loginapp_reqCreateAccount : Message
	{
		// Token: 0x06005F63 RID: 24419 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_reqCreateAccount(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
