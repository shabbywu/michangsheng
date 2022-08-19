using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C04 RID: 3076
	public class Message_Client_onUpdateData_p_optimized : Message
	{
		// Token: 0x060054C1 RID: 21697 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x002359E8 File Offset: 0x00233BE8
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_p_optimized(msgstream);
		}
	}
}
