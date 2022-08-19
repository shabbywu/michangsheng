using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C06 RID: 3078
	public class Message_Client_onUpdateData_xz_optimized : Message
	{
		// Token: 0x060054C5 RID: 21701 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00235A02 File Offset: 0x00233C02
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_optimized(msgstream);
		}
	}
}
