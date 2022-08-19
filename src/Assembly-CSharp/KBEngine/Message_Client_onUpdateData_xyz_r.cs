using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFE RID: 3070
	public class Message_Client_onUpdateData_xyz_r : Message
	{
		// Token: 0x060054B5 RID: 21685 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0023599A File Offset: 0x00233B9A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_r(msgstream);
		}
	}
}
