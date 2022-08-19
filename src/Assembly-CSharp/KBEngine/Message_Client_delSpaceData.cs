using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1A RID: 3098
	public class Message_Client_delSpaceData : Message
	{
		// Token: 0x060054ED RID: 21741 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_delSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00235B2C File Offset: 0x00233D2C
		public override void handleMessage(MemoryStream msgstream)
		{
			uint spaceID = msgstream.readUint32();
			string key = msgstream.readString();
			KBEngineApp.app.Client_delSpaceData(spaceID, key);
		}
	}
}
