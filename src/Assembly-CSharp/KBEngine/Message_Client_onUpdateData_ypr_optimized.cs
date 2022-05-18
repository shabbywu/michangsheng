using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F84 RID: 3972
	public class Message_Client_onUpdateData_ypr_optimized : Message
	{
		// Token: 0x06005EF5 RID: 24309 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00042788 File Offset: 0x00040988
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_ypr_optimized(msgstream);
		}
	}
}
