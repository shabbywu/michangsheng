using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7B RID: 3963
	public class Message_Client_onUpdateData_xz_r : Message
	{
		// Token: 0x06005EE3 RID: 24291 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x00042713 File Offset: 0x00040913
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_r(msgstream);
		}
	}
}
