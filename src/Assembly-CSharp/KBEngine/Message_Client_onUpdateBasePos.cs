using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F69 RID: 3945
	public class Message_Client_onUpdateBasePos : Message
	{
		// Token: 0x06005EBF RID: 24255 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateBasePos(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x00262D58 File Offset: 0x00260F58
		public override void handleMessage(MemoryStream msgstream)
		{
			float x = msgstream.readFloat();
			float y = msgstream.readFloat();
			float z = msgstream.readFloat();
			KBEngineApp.app.Client_onUpdateBasePos(x, y, z);
		}
	}
}
