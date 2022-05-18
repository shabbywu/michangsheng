using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F9A RID: 3994
	public class Message_Client_onUpdateData_xyz_r_optimized : Message
	{
		// Token: 0x06005F21 RID: 24353 RVA: 0x00042600 File Offset: 0x00040800
		public Message_Client_onUpdateData_xyz_r_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x000428A6 File Offset: 0x00040AA6
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xyz_r_optimized(msgstream);
		}
	}
}
