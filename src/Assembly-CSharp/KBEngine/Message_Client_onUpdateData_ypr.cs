using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BE8 RID: 3048
	public class Message_Client_onUpdateData_ypr : Message
	{
		// Token: 0x06005489 RID: 21641 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_ypr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0023587C File Offset: 0x00233A7C
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_ypr(msgstream);
		}
	}
}
