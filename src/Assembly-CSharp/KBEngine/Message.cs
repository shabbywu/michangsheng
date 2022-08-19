using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000BDE RID: 3038
	public class Message
	{
		// Token: 0x06005475 RID: 21621 RVA: 0x00235772 File Offset: 0x00233972
		public Message(ushort msgid, string msgname, short length, sbyte argstype, List<byte> msgargtypes)
		{
			this.id = msgid;
			this.name = msgname;
			this.msglen = length;
			this.argsType = argstype;
			this.argtypes = msgargtypes;
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void handleMessage(MemoryStream msgstream)
		{
		}

		// Token: 0x04005087 RID: 20615
		public ushort id;

		// Token: 0x04005088 RID: 20616
		public string name;

		// Token: 0x04005089 RID: 20617
		public short msglen = -1;

		// Token: 0x0400508A RID: 20618
		public List<byte> argtypes;

		// Token: 0x0400508B RID: 20619
		public sbyte argsType;
	}
}
