using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BDF RID: 3039
	public class Message_Client_onReloginBaseappFailed : Message
	{
		// Token: 0x06005477 RID: 21623 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onReloginBaseappFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x002357B8 File Offset: 0x002339B8
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReloginBaseappFailed(failedcode);
		}
	}
}
