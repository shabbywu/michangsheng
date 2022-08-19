using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF4 RID: 3060
	public class Message_Client_onUpdateData_xz_y : Message
	{
		// Token: 0x060054A1 RID: 21665 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00235918 File Offset: 0x00233B18
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_y(msgstream);
		}
	}
}
