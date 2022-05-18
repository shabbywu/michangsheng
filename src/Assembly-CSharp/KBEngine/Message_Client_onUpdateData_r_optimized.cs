using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F8A RID: 3978
	public class Message_Client_onUpdateData_r_optimized : Message
	{
		// Token: 0x06005F01 RID: 24321 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x000427D6 File Offset: 0x000409D6
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_r_optimized(msgstream);
		}
	}
}
