using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BFA RID: 3066
	public class Message_Client_onUpdateData_xyz_yr : Message
	{
		// Token: 0x060054AD RID: 21677 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz_yr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00235966 File Offset: 0x00233B66
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yr(msgstream);
		}
	}
}
