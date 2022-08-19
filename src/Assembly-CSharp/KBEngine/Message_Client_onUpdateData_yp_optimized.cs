using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C00 RID: 3072
	public class Message_Client_onUpdateData_yp_optimized : Message
	{
		// Token: 0x060054B9 RID: 21689 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x002359B4 File Offset: 0x00233BB4
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yp_optimized(msgstream);
		}
	}
}
