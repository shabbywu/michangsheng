using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C2C RID: 3116
	public class Message_Client_onStreamDataStarted : Message
	{
		// Token: 0x06005511 RID: 21777 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onStreamDataStarted(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00235CD8 File Offset: 0x00233ED8
		public override void handleMessage(MemoryStream msgstream)
		{
			short id = msgstream.readInt16();
			uint datasize = msgstream.readUint32();
			string descr = msgstream.readString();
			KBEngineApp.app.Client_onStreamDataStarted(id, datasize, descr);
		}
	}
}
