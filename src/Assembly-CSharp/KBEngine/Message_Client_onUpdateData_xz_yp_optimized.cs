using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C08 RID: 3080
	public class Message_Client_onUpdateData_xz_yp_optimized : Message
	{
		// Token: 0x060054C9 RID: 21705 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00235A1C File Offset: 0x00233C1C
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yp_optimized(msgstream);
		}
	}
}
