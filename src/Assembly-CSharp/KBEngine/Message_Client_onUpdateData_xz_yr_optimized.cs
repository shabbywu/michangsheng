using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F8E RID: 3982
	public class Message_Client_onUpdateData_xz_yr_optimized : Message
	{
		// Token: 0x06005F09 RID: 24329 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x0004280A File Offset: 0x00040A0A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yr_optimized(msgstream);
		}
	}
}
