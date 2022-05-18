using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC0 RID: 4032
	public class Message_Loginapp_importClientSDK : Message
	{
		// Token: 0x06005F6D RID: 24429 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_importClientSDK(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
