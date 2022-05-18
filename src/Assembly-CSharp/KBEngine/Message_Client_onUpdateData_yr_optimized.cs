using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F86 RID: 3974
	public class Message_Client_onUpdateData_yr_optimized : Message
	{
		// Token: 0x06005EF9 RID: 24313 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x000427A2 File Offset: 0x000409A2
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_yr_optimized(msgstream);
		}
	}
}
