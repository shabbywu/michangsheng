using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FAB RID: 4011
	public class Message_Client_onEntityEnterWorld : Message
	{
		// Token: 0x06005F43 RID: 24387 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onEntityEnterWorld(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x00042927 File Offset: 0x00040B27
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onEntityEnterWorld(msgstream);
		}
	}
}
