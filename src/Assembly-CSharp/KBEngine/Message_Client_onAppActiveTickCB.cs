using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA4 RID: 4004
	public class Message_Client_onAppActiveTickCB : Message
	{
		// Token: 0x06005F35 RID: 24373 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onAppActiveTickCB(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x000428E7 File Offset: 0x00040AE7
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onAppActiveTickCB();
		}
	}
}
