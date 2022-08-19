using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C07 RID: 3079
	public class Message_Client_onUpdateData_xz_ypr_optimized : Message
	{
		// Token: 0x060054C7 RID: 21703 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x00235A0F File Offset: 0x00233C0F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_ypr_optimized(msgstream);
		}
	}
}
