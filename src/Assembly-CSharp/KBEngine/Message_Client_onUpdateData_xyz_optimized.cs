using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0E RID: 3086
	public class Message_Client_onUpdateData_xyz_optimized : Message
	{
		// Token: 0x060054D5 RID: 21717 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x00235A6A File Offset: 0x00233C6A
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_optimized(msgstream);
		}
	}
}
