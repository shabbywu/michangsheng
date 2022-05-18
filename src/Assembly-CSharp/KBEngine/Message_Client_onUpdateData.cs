using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F6C RID: 3948
	public class Message_Client_onUpdateData : Message
	{
		// Token: 0x06005EC5 RID: 24261 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x00042650 File Offset: 0x00040850
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData(msgstream);
		}
	}
}
