using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F91 RID: 3985
	public class Message_Client_onUpdateData_xz_p_optimized : Message
	{
		// Token: 0x06005F0F RID: 24335 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xz_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x00042831 File Offset: 0x00040A31
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_p_optimized(msgstream);
		}
	}
}
