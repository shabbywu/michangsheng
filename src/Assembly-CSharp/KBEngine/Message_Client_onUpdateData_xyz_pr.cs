using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFB RID: 3067
	public class Message_Client_onUpdateData_xyz_pr : Message
	{
		// Token: 0x060054AF RID: 21679 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x00235973 File Offset: 0x00233B73
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_pr(msgstream);
		}
	}
}
