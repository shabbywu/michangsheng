using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BEA RID: 3050
	public class Message_Client_onUpdateData_yr : Message
	{
		// Token: 0x0600548D RID: 21645 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_yr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x00235896 File Offset: 0x00233A96
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yr(msgstream);
		}
	}
}
