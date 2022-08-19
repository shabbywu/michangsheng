using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF8 RID: 3064
	public class Message_Client_onUpdateData_xyz_ypr : Message
	{
		// Token: 0x060054A9 RID: 21673 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0023594C File Offset: 0x00233B4C
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_ypr(msgstream);
		}
	}
}
