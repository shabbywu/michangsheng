using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F89 RID: 3977
	public class Message_Client_onUpdateData_p_optimized : Message
	{
		// Token: 0x06005EFF RID: 24319 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x000427C9 File Offset: 0x000409C9
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_p_optimized(msgstream);
		}
	}
}
