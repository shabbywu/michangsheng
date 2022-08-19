using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C31 RID: 3121
	public class Message_Client_onImportClientEntityDef : Message
	{
		// Token: 0x0600551B RID: 21787 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onImportClientEntityDef(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00235D60 File Offset: 0x00233F60
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientEntityDef(msgstream);
		}
	}
}
