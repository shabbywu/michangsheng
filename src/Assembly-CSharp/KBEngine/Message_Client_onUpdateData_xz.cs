using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F74 RID: 3956
	public class Message_Client_onUpdateData_xz : Message
	{
		// Token: 0x06005ED5 RID: 24277 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x000426B8 File Offset: 0x000408B8
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz(msgstream);
		}
	}
}
