using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F93 RID: 3987
	public class Message_Client_onUpdateData_xyz_optimized : Message
	{
		// Token: 0x06005F13 RID: 24339 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x0004284B File Offset: 0x00040A4B
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_optimized(msgstream);
		}
	}
}
