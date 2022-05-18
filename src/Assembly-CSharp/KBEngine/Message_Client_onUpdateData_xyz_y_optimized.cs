using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F98 RID: 3992
	public class Message_Client_onUpdateData_xyz_y_optimized : Message
	{
		// Token: 0x06005F1D RID: 24349 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0004288C File Offset: 0x00040A8C
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_y_optimized(msgstream);
		}
	}
}
