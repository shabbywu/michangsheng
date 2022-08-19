using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C12 RID: 3090
	public class Message_Client_onUpdateData_xyz_pr_optimized : Message
	{
		// Token: 0x060054DD RID: 21725 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_pr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00235A9E File Offset: 0x00233C9E
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_pr_optimized(msgstream);
		}
	}
}
