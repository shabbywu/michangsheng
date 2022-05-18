using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9D RID: 3997
	public class Message_Client_initSpaceData : Message
	{
		// Token: 0x06005F27 RID: 24359 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_initSpaceData(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x000428CD File Offset: 0x00040ACD
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_initSpaceData(msgstream);
		}
	}
}
