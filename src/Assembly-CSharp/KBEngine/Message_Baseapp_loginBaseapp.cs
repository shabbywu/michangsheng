using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FCB RID: 4043
	public class Message_Baseapp_loginBaseapp : Message
	{
		// Token: 0x06005F83 RID: 24451 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Baseapp_loginBaseapp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
