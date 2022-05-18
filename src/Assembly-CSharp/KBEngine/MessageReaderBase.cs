using System;

namespace KBEngine
{
	// Token: 0x02000F5F RID: 3935
	public abstract class MessageReaderBase
	{
		// Token: 0x06005EAE RID: 24238
		public abstract void process(byte[] datas, uint offset, uint length);
	}
}
