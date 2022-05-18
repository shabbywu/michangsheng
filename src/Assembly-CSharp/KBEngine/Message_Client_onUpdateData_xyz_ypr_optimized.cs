using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F94 RID: 3988
	public class Message_Client_onUpdateData_xyz_ypr_optimized : Message
	{
		// Token: 0x06005F15 RID: 24341 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_ypr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x00042858 File Offset: 0x00040A58
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_ypr_optimized(msgstream);
		}
	}
}
