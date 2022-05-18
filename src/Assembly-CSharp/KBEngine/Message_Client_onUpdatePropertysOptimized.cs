using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F67 RID: 3943
	public class Message_Client_onUpdatePropertysOptimized : Message
	{
		// Token: 0x06005EBB RID: 24251 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdatePropertysOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x00042629 File Offset: 0x00040829
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdatePropertysOptimized(msgstream);
		}
	}
}
