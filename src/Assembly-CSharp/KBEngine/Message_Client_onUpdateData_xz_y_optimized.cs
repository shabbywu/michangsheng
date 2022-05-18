using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F90 RID: 3984
	public class Message_Client_onUpdateData_xz_y_optimized : Message
	{
		// Token: 0x06005F0D RID: 24333 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x00042824 File Offset: 0x00040A24
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_y_optimized(msgstream);
		}
	}
}
