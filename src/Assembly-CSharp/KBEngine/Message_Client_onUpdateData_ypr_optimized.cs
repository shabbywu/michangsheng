using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFF RID: 3071
	public class Message_Client_onUpdateData_ypr_optimized : Message
	{
		// Token: 0x060054B7 RID: 21687 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x002359A7 File Offset: 0x00233BA7
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_ypr_optimized(msgstream);
		}
	}
}
