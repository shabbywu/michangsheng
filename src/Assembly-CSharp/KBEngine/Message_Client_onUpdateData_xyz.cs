using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF7 RID: 3063
	public class Message_Client_onUpdateData_xyz : Message
	{
		// Token: 0x060054A7 RID: 21671 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xyz(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0023593F File Offset: 0x00233B3F
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz(msgstream);
		}
	}
}
