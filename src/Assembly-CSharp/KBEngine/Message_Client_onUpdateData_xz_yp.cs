using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F76 RID: 3958
	public class Message_Client_onUpdateData_xz_yp : Message
	{
		// Token: 0x06005ED9 RID: 24281 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x000426D2 File Offset: 0x000408D2
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yp(msgstream);
		}
	}
}
