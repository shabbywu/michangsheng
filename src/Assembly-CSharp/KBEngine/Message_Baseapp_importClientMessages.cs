﻿using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C4A RID: 3146
	public class Message_Baseapp_importClientMessages : Message
	{
		// Token: 0x0600554D RID: 21837 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_importClientMessages(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
