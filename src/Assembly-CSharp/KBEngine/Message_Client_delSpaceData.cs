using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9F RID: 3999
	public class Message_Client_delSpaceData : Message
	{
		// Token: 0x06005F2B RID: 24363 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_delSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x00262DE0 File Offset: 0x00260FE0
		public override void handleMessage(MemoryStream msgstream)
		{
			uint spaceID = msgstream.readUint32();
			string key = msgstream.readString();
			KBEngineApp.app.Client_delSpaceData(spaceID, key);
		}
	}
}
