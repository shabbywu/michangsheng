using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D6 RID: 4566
	public class ProtocolMessage
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06006FB9 RID: 28601 RVA: 0x0004BEB2 File Offset: 0x0004A0B2
		// (set) Token: 0x06006FBA RID: 28602 RVA: 0x0004BEBA File Offset: 0x0004A0BA
		public string type { get; private set; }

		// Token: 0x06006FBB RID: 28603 RVA: 0x0004BEC3 File Offset: 0x0004A0C3
		public ProtocolMessage(string typ)
		{
			this.type = typ;
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x0004BED2 File Offset: 0x0004A0D2
		public ProtocolMessage(string typ, int sq)
		{
			this.type = typ;
			this.seq = sq;
		}

		// Token: 0x040062BF RID: 25279
		public int seq;
	}
}
