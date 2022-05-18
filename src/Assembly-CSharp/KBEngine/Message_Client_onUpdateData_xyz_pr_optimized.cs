using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F97 RID: 3991
	public class Message_Client_onUpdateData_xyz_pr_optimized : Message
	{
		// Token: 0x06005F1B RID: 24347 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_pr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x0004287F File Offset: 0x00040A7F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_pr_optimized(msgstream);
		}
	}
}
