using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F77 RID: 3959
	public class Message_Client_onUpdateData_xz_yr : Message
	{
		// Token: 0x06005EDB RID: 24283 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_yr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x000426DF File Offset: 0x000408DF
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yr(msgstream);
		}
	}
}
