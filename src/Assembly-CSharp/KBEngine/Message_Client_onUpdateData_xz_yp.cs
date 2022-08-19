using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF1 RID: 3057
	public class Message_Client_onUpdateData_xz_yp : Message
	{
		// Token: 0x0600549B RID: 21659 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x002358F1 File Offset: 0x00233AF1
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yp(msgstream);
		}
	}
}
