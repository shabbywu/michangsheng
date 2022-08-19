using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF0 RID: 3056
	public class Message_Client_onUpdateData_xz_ypr : Message
	{
		// Token: 0x06005499 RID: 21657 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x002358E4 File Offset: 0x00233AE4
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_ypr(msgstream);
		}
	}
}
