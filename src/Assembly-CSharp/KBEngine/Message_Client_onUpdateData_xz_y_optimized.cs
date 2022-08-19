using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C0B RID: 3083
	public class Message_Client_onUpdateData_xz_y_optimized : Message
	{
		// Token: 0x060054CF RID: 21711 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x00235A43 File Offset: 0x00233C43
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_y_optimized(msgstream);
		}
	}
}
