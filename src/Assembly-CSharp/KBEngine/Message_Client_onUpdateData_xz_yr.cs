using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BF2 RID: 3058
	public class Message_Client_onUpdateData_xz_yr : Message
	{
		// Token: 0x0600549D RID: 21661 RVA: 0x002357A6 File Offset: 0x002339A6
		public Message_Client_onUpdateData_xz_yr(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes) : base(msgid, msgname, length, argstype, msgargtypes)
		{
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x002358FE File Offset: 0x00233AFE
		public override void handleMessage(MemoryStream msgstream)
		{
			KBEngineApp.app.Client_onUpdateData_xz_yr(msgstream);
		}
	}
}
