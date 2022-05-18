using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F6B RID: 3947
	public class Message_Client_onUpdateBasePosXZ : Message
	{
		// Token: 0x06005EC3 RID: 24259 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateBasePosXZ(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x00262D88 File Offset: 0x00260F88
		public override void handleMessage(MemoryStream msgstream)
		{
			float x = msgstream.readFloat();
			float z = msgstream.readFloat();
			KBEngineApp.app.Client_onUpdateBasePosXZ(x, z);
		}
	}
}
