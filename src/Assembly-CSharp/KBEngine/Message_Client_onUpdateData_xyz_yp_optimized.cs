using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F95 RID: 3989
	public class Message_Client_onUpdateData_xyz_yp_optimized : Message
	{
		// Token: 0x06005F17 RID: 24343 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_yp_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x00042865 File Offset: 0x00040A65
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yp_optimized(msgstream);
		}
	}
}
