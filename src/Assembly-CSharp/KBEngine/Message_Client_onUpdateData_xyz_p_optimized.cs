using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C14 RID: 3092
	public class Message_Client_onUpdateData_xyz_p_optimized : Message
	{
		// Token: 0x060054E1 RID: 21729 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00235AB8 File Offset: 0x00233CB8
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_p_optimized(msgstream);
		}
	}
}
