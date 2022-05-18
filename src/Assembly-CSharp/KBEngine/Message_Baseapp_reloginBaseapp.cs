using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCC RID: 4044
	public class Message_Baseapp_reloginBaseapp : Message
	{
		// Token: 0x06005F85 RID: 24453 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_reloginBaseapp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
