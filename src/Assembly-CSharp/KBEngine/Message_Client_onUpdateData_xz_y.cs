using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F79 RID: 3961
	public class Message_Client_onUpdateData_xz_y : Message
	{
		// Token: 0x06005EDF RID: 24287 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x000426F9 File Offset: 0x000408F9
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_y(msgstream);
		}
	}
}
