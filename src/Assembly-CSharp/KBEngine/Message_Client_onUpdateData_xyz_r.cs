using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F83 RID: 3971
	public class Message_Client_onUpdateData_xyz_r : Message
	{
		// Token: 0x06005EF3 RID: 24307 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x0004277B File Offset: 0x0004097B
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_r(msgstream);
		}
	}
}
