using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FC1 RID: 4033
	public class Message_Loginapp_importServerErrorsDescr : Message
	{
		// Token: 0x06005F6F RID: 24431 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Loginapp_importServerErrorsDescr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x000042DD File Offset: 0x000024DD
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
