using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0A RID: 3082
	public class Message_Client_onUpdateData_xz_pr_optimized : Message
	{
		// Token: 0x060054CD RID: 21709 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_pr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00235A36 File Offset: 0x00233C36
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_pr_optimized(msgstream);
		}
	}
}
