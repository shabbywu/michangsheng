using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F81 RID: 3969
	public class Message_Client_onUpdateData_xyz_y : Message
	{
		// Token: 0x06005EEF RID: 24303 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x00042761 File Offset: 0x00040961
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_y(msgstream);
		}
	}
}
