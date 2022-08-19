using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C16 RID: 3094
	public class Message_Client_onImportServerErrorsDescr : Message
	{
		// Token: 0x060054E5 RID: 21733 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onImportServerErrorsDescr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00235AD2 File Offset: 0x00233CD2
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onImportServerErrorsDescr(msgstream);
		}
	}
}
