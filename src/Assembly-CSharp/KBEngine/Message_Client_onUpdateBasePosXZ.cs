using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE6 RID: 3046
	public class Message_Client_onUpdateBasePosXZ : Message
	{
		// Token: 0x06005485 RID: 21637 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateBasePosXZ(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x00235848 File Offset: 0x00233A48
		public override void handleMessage(MemoryStream msgstream)
		{
			float x = msgstream.readFloat();
			float z = msgstream.readFloat();
			KBEngineApp.app.Client_onUpdateBasePosXZ(x, z);
		}
	}
}
