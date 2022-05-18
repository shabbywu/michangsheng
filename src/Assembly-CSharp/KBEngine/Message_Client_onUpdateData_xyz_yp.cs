using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7E RID: 3966
	public class Message_Client_onUpdateData_xyz_yp : Message
	{
		// Token: 0x06005EE9 RID: 24297 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_yp(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0004273A File Offset: 0x0004093A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yp(msgstream);
		}
	}
}
