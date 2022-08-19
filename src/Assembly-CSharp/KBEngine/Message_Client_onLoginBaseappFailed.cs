using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C24 RID: 3108
	public class Message_Client_onLoginBaseappFailed : Message
	{
		// Token: 0x06005501 RID: 21761 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onLoginBaseappFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00235C24 File Offset: 0x00233E24
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onLoginBaseappFailed(failedcode);
		}
	}
}
