using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F78 RID: 3960
	public class Message_Client_onUpdateData_xz_pr : Message
	{
		// Token: 0x06005EDD RID: 24285 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x000426EC File Offset: 0x000408EC
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_pr(msgstream);
		}
	}
}
