using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BEE RID: 3054
	public class Message_Client_onUpdateData_r : Message
	{
		// Token: 0x06005495 RID: 21653 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x002358CA File Offset: 0x00233ACA
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_r(msgstream);
		}
	}
}
