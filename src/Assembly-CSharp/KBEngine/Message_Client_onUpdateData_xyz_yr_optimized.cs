using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F96 RID: 3990
	public class Message_Client_onUpdateData_xyz_yr_optimized : Message
	{
		// Token: 0x06005F19 RID: 24345 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_yr_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00042872 File Offset: 0x00040A72
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_yr_optimized(msgstream);
		}
	}
}
