using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB1 RID: 4017
	public class Message_Client_onStreamDataStarted : Message
	{
		// Token: 0x06005F4F RID: 24399 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onStreamDataStarted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x00262F18 File Offset: 0x00261118
		public override void handleMessage(MemoryStream msgstream)
		{
			short id = msgstream.readInt16();
			uint datasize = msgstream.readUint32();
			string descr = msgstream.readString();
			KBEngineApp.app.Client_onStreamDataStarted(id, datasize, descr);
		}
	}
}
