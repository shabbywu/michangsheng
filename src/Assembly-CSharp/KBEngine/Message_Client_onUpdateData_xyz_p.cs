using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F82 RID: 3970
	public class Message_Client_onUpdateData_xyz_p : Message
	{
		// Token: 0x06005EF1 RID: 24305 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_p(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0004276E File Offset: 0x0004096E
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_p(msgstream);
		}
	}
}
