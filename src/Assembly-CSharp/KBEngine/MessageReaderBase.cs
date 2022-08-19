using System;

namespace KBEngine
{
	// Token: 0x02000BDB RID: 3035
	public abstract class MessageReaderBase
	{
		// Token: 0x06005470 RID: 21616
		public abstract void process(byte[] datas, uint offset, uint length);
	}
}
