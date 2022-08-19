using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE4 RID: 3044
	public class Message_Client_onUpdateBasePos : Message
	{
		// Token: 0x06005481 RID: 21633 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateBasePos(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0023580C File Offset: 0x00233A0C
		public override void handleMessage(MemoryStream msgstream)
		{
			float x = msgstream.readFloat();
			float y = msgstream.readFloat();
			float z = msgstream.readFloat();
			KBEngineApp.app.Client_onUpdateBasePos(x, y, z);
		}
	}
}
