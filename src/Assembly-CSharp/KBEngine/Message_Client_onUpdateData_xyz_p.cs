using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFD RID: 3069
	public class Message_Client_onUpdateData_xyz_p : Message
	{
		// Token: 0x060054B3 RID: 21683 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_p(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0023598D File Offset: 0x00233B8D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_p(msgstream);
		}
	}
}
