﻿using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C41 RID: 3137
	public class Message_Baseapp_onUpdateDataFromClientForControlledEntity : Message
	{
		// Token: 0x0600553B RID: 21819 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Baseapp_onUpdateDataFromClientForControlledEntity(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
