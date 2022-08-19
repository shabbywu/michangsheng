using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFC RID: 3068
	public class Message_Client_onUpdateData_xyz_y : Message
	{
		// Token: 0x060054B1 RID: 21681 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00235980 File Offset: 0x00233B80
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_y(msgstream);
		}
	}
}
