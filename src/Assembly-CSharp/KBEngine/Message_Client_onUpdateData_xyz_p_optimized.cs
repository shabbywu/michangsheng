using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F99 RID: 3993
	public class Message_Client_onUpdateData_xyz_p_optimized : Message
	{
		// Token: 0x06005F1F RID: 24351 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_p_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x00042899 File Offset: 0x00040A99
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_p_optimized(msgstream);
		}
	}
}
