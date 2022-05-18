using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F7C RID: 3964
	public class Message_Client_onUpdateData_xyz : Message
	{
		// Token: 0x06005EE5 RID: 24293 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x00042720 File Offset: 0x00040920
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz(msgstream);
		}
	}
}
