using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BEF RID: 3055
	public class Message_Client_onUpdateData_xz : Message
	{
		// Token: 0x06005497 RID: 21655 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x002358D7 File Offset: 0x00233AD7
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz(msgstream);
		}
	}
}
