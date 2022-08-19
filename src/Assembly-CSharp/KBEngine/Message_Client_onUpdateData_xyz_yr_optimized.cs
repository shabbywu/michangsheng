using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C11 RID: 3089
	public class Message_Client_onUpdateData_xyz_yr_optimized : Message
	{
		// Token: 0x060054DB RID: 21723 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x00235A91 File Offset: 0x00233C91
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yr_optimized(msgstream);
		}
	}
}
