using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F8D RID: 3981
	public class Message_Client_onUpdateData_xz_yp_optimized : Message
	{
		// Token: 0x06005F07 RID: 24327 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x000427FD File Offset: 0x000409FD
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yp_optimized(msgstream);
		}
	}
}
