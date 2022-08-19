using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C03 RID: 3075
	public class Message_Client_onUpdateData_y_optimized : Message
	{
		// Token: 0x060054BF RID: 21695 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_y_optimized(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x002359DB File Offset: 0x00233BDB
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_y_optimized(msgstream);
		}
	}
}
