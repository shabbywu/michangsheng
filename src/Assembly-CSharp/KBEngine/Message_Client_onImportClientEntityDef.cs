using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FB6 RID: 4022
	public class Message_Client_onImportClientEntityDef : Message
	{
		// Token: 0x06005F59 RID: 24409 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onImportClientEntityDef(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x00042968 File Offset: 0x00040B68
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientEntityDef(msgstream);
		}
	}
}
