using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000F63 RID: 3939
	public class Message
	{
		// Token: 0x06005EB3 RID: 24243 RVA: 0x000425CC File Offset: 0x000407CC
		public Message(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		{
			this.id = msgid;
			this.name = msgname;
			this.msglen = length;
			this.argsType = argstype;
			this.argtypes = msgargtypes;
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void handleMessage(MemoryStream msgstream)
		{
		}

		// Token: 0x04005B30 RID: 23344
		public ushort id;

		// Token: 0x04005B31 RID: 23345
		public string name;

		// Token: 0x04005B32 RID: 23346
		public short msglen = -1;

		// Token: 0x04005B33 RID: 23347
		public List<byte> argtypes;

		// Token: 0x04005B34 RID: 23348
		public sbyte argsType;
	}
}
