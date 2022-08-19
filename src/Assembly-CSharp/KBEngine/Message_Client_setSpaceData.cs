using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C19 RID: 3097
	public class Message_Client_setSpaceData : Message
	{
		// Token: 0x060054EB RID: 21739 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_setSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x00235AFC File Offset: 0x00233CFC
		public override void handleMessage(MemoryStream msgstream)
		{
			uint spaceID = msgstream.readUint32();
			string key = msgstream.readString();
			string value = msgstream.readString();
			KBEngineApp.app.Client_setSpaceData(spaceID, key, value);
		}
	}
}
