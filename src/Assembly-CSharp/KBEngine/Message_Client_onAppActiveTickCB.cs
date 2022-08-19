using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C1F RID: 3103
	public class Message_Client_onAppActiveTickCB : Message
	{
		// Token: 0x060054F7 RID: 21751 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onAppActiveTickCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x00235BC0 File Offset: 0x00233DC0
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onAppActiveTickCB();
		}
	}
}
