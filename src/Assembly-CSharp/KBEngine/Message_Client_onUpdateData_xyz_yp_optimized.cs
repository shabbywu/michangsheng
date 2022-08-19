using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C10 RID: 3088
	public class Message_Client_onUpdateData_xyz_yp_optimized : Message
	{
		// Token: 0x060054D9 RID: 21721 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x00235A84 File Offset: 0x00233C84
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yp_optimized(msgstream);
		}
	}
}
