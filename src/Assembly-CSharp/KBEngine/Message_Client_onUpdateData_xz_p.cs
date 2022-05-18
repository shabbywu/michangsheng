using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7A RID: 3962
	public class Message_Client_onUpdateData_xz_p : Message
	{
		// Token: 0x06005EE1 RID: 24289 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_p(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00042706 File Offset: 0x00040906
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_p(msgstream);
		}
	}
}
