using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7D RID: 3965
	public class Message_Client_onUpdateData_xyz_ypr : Message
	{
		// Token: 0x06005EE7 RID: 24295 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0004272D File Offset: 0x0004092D
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_ypr(msgstream);
		}
	}
}
