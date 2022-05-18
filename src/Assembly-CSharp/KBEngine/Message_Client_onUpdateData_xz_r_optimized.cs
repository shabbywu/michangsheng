using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F92 RID: 3986
	public class Message_Client_onUpdateData_xz_r_optimized : Message
	{
		// Token: 0x06005F11 RID: 24337 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x0004283E File Offset: 0x00040A3E
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_r_optimized(msgstream);
		}
	}
}
