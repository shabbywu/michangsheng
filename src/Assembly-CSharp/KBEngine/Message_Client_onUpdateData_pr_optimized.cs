using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C02 RID: 3074
	public class Message_Client_onUpdateData_pr_optimized : Message
	{
		// Token: 0x060054BD RID: 21693 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_pr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x002359CE File Offset: 0x00233BCE
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_pr_optimized(msgstream);
		}
	}
}
