using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF6 RID: 3062
	public class Message_Client_onUpdateData_xz_r : Message
	{
		// Token: 0x060054A5 RID: 21669 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x00235932 File Offset: 0x00233B32
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_r(msgstream);
		}
	}
}
