using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF3 RID: 3059
	public class Message_Client_onUpdateData_xz_pr : Message
	{
		// Token: 0x0600549F RID: 21663 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0023590B File Offset: 0x00233B0B
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_pr(msgstream);
		}
	}
}
