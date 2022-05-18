using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F80 RID: 3968
	public class Message_Client_onUpdateData_xyz_pr : Message
	{
		// Token: 0x06005EED RID: 24301 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x00042754 File Offset: 0x00040954
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_pr(msgstream);
		}
	}
}
