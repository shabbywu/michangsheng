using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C13 RID: 3091
	public class Message_Client_onUpdateData_xyz_y_optimized : Message
	{
		// Token: 0x060054DF RID: 21727 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x00235AAB File Offset: 0x00233CAB
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_y_optimized(msgstream);
		}
	}
}
