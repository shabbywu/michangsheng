using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F71 RID: 3953
	public class Message_Client_onUpdateData_y : Message
	{
		// Token: 0x06005ECF RID: 24271 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x00042691 File Offset: 0x00040891
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_y(msgstream);
		}
	}
}
