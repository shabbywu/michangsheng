using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BEC RID: 3052
	public class Message_Client_onUpdateData_y : Message
	{
		// Token: 0x06005491 RID: 21649 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_y(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x002358B0 File Offset: 0x00233AB0
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_y(msgstream);
		}
	}
}
