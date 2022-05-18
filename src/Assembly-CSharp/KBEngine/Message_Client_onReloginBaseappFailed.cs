using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F64 RID: 3940
	public class Message_Client_onReloginBaseappFailed : Message
	{
		// Token: 0x06005EB5 RID: 24245 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onReloginBaseappFailed(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x00262D38 File Offset: 0x00260F38
		public override void handleMessage(MemoryStream msgstream)
		{
			ushort failedcode = msgstream.readUint16();
			KBEngineApp.app.Client_onReloginBaseappFailed(failedcode);
		}
	}
}
