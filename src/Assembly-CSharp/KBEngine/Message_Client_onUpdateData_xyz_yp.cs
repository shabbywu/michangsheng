using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF9 RID: 3065
	public class Message_Client_onUpdateData_xyz_yp : Message
	{
		// Token: 0x060054AB RID: 21675 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00235959 File Offset: 0x00233B59
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yp(msgstream);
		}
	}
}
