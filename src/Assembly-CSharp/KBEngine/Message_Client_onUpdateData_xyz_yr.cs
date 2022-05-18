using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7F RID: 3967
	public class Message_Client_onUpdateData_xyz_yr : Message
	{
		// Token: 0x06005EEB RID: 24299 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_yr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x00042747 File Offset: 0x00040947
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yr(msgstream);
		}
	}
}
