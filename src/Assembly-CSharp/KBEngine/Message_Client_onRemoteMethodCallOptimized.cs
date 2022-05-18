using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F66 RID: 3942
	public class Message_Client_onRemoteMethodCallOptimized : Message
	{
		// Token: 0x06005EB9 RID: 24249 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onRemoteMethodCallOptimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x0004261C File Offset: 0x0004081C
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onRemoteMethodCallOptimized(msgstream);
		}
	}
}
