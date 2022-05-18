using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9C RID: 3996
	public class Message_Client_onImportClientSDK : Message
	{
		// Token: 0x06005F25 RID: 24357 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onImportClientSDK(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x000428C0 File Offset: 0x00040AC0
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportClientSDK(msgstream);
		}
	}
}
