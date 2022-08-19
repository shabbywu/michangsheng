using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C17 RID: 3095
	public class Message_Client_onImportClientSDK : Message
	{
		// Token: 0x060054E7 RID: 21735 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onImportClientSDK(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00235ADF File Offset: 0x00233CDF
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientSDK(msgstream);
		}
	}
}
