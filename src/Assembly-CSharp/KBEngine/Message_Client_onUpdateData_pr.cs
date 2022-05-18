using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F70 RID: 3952
	public class Message_Client_onUpdateData_pr : Message
	{
		// Token: 0x06005ECD RID: 24269 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_pr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ECE RID: 24270 RVA: 0x00042684 File Offset: 0x00040884
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_pr(msgstream);
		}
	}
}
