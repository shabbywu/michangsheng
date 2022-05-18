using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F68 RID: 3944
	public class Message_Client_onSetEntityPosAndDir : Message
	{
		// Token: 0x06005EBD RID: 24253 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onSetEntityPosAndDir(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x00042636 File Offset: 0x00040836
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onSetEntityPosAndDir(msgstream);
		}
	}
}
