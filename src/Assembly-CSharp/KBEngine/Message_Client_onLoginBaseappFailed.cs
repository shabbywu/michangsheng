using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000FA9 RID: 4009
	public class Message_Client_onLoginBaseappFailed : Message
	{
		// Token: 0x06005F3F RID: 24383 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onLoginBaseappFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x00262E98 File Offset: 0x00261098
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onLoginBaseappFailed(failedcode);
		}
	}
}
