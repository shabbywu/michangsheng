using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C05 RID: 3077
	public class Message_Client_onUpdateData_r_optimized : Message
	{
		// Token: 0x060054C3 RID: 21699 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x002359F5 File Offset: 0x00233BF5
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_r_optimized(msgstream);
		}
	}
}
