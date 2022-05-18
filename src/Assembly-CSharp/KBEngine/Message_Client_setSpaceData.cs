using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9E RID: 3998
	public class Message_Client_setSpaceData : Message
	{
		// Token: 0x06005F29 RID: 24361 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_setSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x00262DB0 File Offset: 0x00260FB0
		public override void handleMessage(MemoryStream msgstream)
		{
			uint spaceID = msgstream.readUint32();
			string key = msgstream.readString();
			string value = msgstream.readString();
			KBEngineApp.app.Client_setSpaceData(spaceID, key, value);
		}
	}
}
