using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9B RID: 3995
	public class Message_Client_onImportServerErrorsDescr : Message
	{
		// Token: 0x06005F23 RID: 24355 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onImportServerErrorsDescr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x000428B3 File Offset: 0x00040AB3
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportServerErrorsDescr(msgstream);
		}
	}
}
