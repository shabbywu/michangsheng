using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DAA RID: 3498
	public class ProtocolMessage
	{
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06006373 RID: 25459 RVA: 0x0027AF64 File Offset: 0x00279164
		// (set) Token: 0x06006374 RID: 25460 RVA: 0x0027AF6C File Offset: 0x0027916C
		public string type { get; private set; }

		// Token: 0x06006375 RID: 25461 RVA: 0x0027AF75 File Offset: 0x00279175
		public ProtocolMessage(string typ)
		{
			this.type = typ;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x0027AF84 File Offset: 0x00279184
		public ProtocolMessage(string typ, int sq)
		{
			this.type = typ;
			this.seq = sq;
		}

		// Token: 0x040055D8 RID: 21976
		public int seq;
	}
}
