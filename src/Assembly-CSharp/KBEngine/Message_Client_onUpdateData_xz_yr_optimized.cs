using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C09 RID: 3081
	public class Message_Client_onUpdateData_xz_yr_optimized : Message
	{
		// Token: 0x060054CB RID: 21707 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x00235A29 File Offset: 0x00233C29
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yr_optimized(msgstream);
		}
	}
}
