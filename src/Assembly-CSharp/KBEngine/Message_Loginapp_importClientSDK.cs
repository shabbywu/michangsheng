using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C3B RID: 3131
	public class Message_Loginapp_importClientSDK : Message
	{
		// Token: 0x0600552F RID: 21807 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_importClientSDK(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
