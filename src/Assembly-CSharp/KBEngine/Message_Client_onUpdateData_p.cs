using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BED RID: 3053
	public class Message_Client_onUpdateData_p : Message
	{
		// Token: 0x06005493 RID: 21651 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_p(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x002358BD File Offset: 0x00233ABD
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_p(msgstream);
		}
	}
}
