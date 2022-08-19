using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C4B RID: 3147
	public class Message_Baseapp_importClientEntityDef : Message
	{
		// Token: 0x0600554F RID: 21839 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_importClientEntityDef(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
