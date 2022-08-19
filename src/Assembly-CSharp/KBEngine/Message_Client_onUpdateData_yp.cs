using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE9 RID: 3049
	public class Message_Client_onUpdateData_yp : Message
	{
		// Token: 0x0600548B RID: 21643 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x00235889 File Offset: 0x00233A89
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yp(msgstream);
		}
	}
}
