using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C3D RID: 3133
	public class Message_Loginapp_onClientActiveTick : Message
	{
		// Token: 0x06005533 RID: 21811 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Loginapp_onClientActiveTick(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00004095 File Offset: 0x00002295
		public override void handleMessage(MemoryStream msgstream)
		{
		}
	}
}
