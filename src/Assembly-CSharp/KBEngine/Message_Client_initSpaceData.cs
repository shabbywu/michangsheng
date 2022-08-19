using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C18 RID: 3096
	public class Message_Client_initSpaceData : Message
	{
		// Token: 0x060054E9 RID: 21737 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_initSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x00235AEC File Offset: 0x00233CEC
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_initSpaceData(msgstream);
		}
	}
}
