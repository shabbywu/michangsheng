using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE5 RID: 3045
	public class Message_Client_onUpdateBaseDir : Message
	{
		// Token: 0x06005483 RID: 21635 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateBaseDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0023583B File Offset: 0x00233A3B
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateBaseDir(msgstream);
		}
	}
}
