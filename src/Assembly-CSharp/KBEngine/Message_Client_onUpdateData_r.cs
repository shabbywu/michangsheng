using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F73 RID: 3955
	public class Message_Client_onUpdateData_r : Message
	{
		// Token: 0x06005ED3 RID: 24275 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_r(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x000426AB File Offset: 0x000408AB
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_r(msgstream);
		}
	}
}
