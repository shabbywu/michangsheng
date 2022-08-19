using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1E RID: 3102
	public class Message_Client_onReloginBaseappSuccessfully : Message
	{
		// Token: 0x060054F5 RID: 21749 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onReloginBaseappSuccessfully(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x00235BB3 File Offset: 0x00233DB3
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onReloginBaseappSuccessfully(msgstream);
		}
	}
}
