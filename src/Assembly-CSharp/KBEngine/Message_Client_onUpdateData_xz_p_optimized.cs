using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0C RID: 3084
	public class Message_Client_onUpdateData_xz_p_optimized : Message
	{
		// Token: 0x060054D1 RID: 21713 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00235A50 File Offset: 0x00233C50
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_p_optimized(msgstream);
		}
	}
}
