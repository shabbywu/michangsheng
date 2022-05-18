using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F88 RID: 3976
	public class Message_Client_onUpdateData_y_optimized : Message
	{
		// Token: 0x06005EFD RID: 24317 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x000427BC File Offset: 0x000409BC
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_y_optimized(msgstream);
		}
	}
}
