using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0D RID: 3085
	public class Message_Client_onUpdateData_xz_r_optimized : Message
	{
		// Token: 0x060054D3 RID: 21715 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x00235A5D File Offset: 0x00233C5D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_r_optimized(msgstream);
		}
	}
}
