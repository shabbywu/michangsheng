using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F8C RID: 3980
	public class Message_Client_onUpdateData_xz_ypr_optimized : Message
	{
		// Token: 0x06005F05 RID: 24325 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x000427F0 File Offset: 0x000409F0
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_ypr_optimized(msgstream);
		}
	}
}
