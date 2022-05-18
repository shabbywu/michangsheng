using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F85 RID: 3973
	public class Message_Client_onUpdateData_yp_optimized : Message
	{
		// Token: 0x06005EF7 RID: 24311 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x00042795 File Offset: 0x00040995
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yp_optimized(msgstream);
		}
	}
}
