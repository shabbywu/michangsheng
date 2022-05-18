using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F75 RID: 3957
	public class Message_Client_onUpdateData_xz_ypr : Message
	{
		// Token: 0x06005ED7 RID: 24279 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x000426C5 File Offset: 0x000408C5
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_ypr(msgstream);
		}
	}
}
