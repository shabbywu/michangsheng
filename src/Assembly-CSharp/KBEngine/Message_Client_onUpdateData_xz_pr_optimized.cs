using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F8F RID: 3983
	public class Message_Client_onUpdateData_xz_pr_optimized : Message
	{
		// Token: 0x06005F0B RID: 24331 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_pr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x00042817 File Offset: 0x00040A17
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_pr_optimized(msgstream);
		}
	}
}
