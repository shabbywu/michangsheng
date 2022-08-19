using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C01 RID: 3073
	public class Message_Client_onUpdateData_yr_optimized : Message
	{
		// Token: 0x060054BB RID: 21691 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x002359C1 File Offset: 0x00233BC1
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yr_optimized(msgstream);
		}
	}
}
