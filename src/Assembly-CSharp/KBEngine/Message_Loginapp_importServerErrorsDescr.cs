using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C3C RID: 3132
	public class Message_Loginapp_importServerErrorsDescr : Message
	{
		// Token: 0x06005531 RID: 21809 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_importServerErrorsDescr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
