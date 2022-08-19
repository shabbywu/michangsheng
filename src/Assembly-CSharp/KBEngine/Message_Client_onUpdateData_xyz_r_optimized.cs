using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C15 RID: 3093
	public class Message_Client_onUpdateData_xyz_r_optimized : Message
	{
		// Token: 0x060054E3 RID: 21731 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00235AC5 File Offset: 0x00233CC5
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_r_optimized(msgstream);
		}
	}
}
