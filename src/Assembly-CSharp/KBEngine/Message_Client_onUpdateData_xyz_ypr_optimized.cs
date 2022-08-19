using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0F RID: 3087
	public class Message_Client_onUpdateData_xyz_ypr_optimized : Message
	{
		// Token: 0x060054D7 RID: 21719 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x00235A77 File Offset: 0x00233C77
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_ypr_optimized(msgstream);
		}
	}
}
