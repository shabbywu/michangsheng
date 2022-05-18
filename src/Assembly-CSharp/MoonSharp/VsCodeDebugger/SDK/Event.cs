using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011DA RID: 4570
	public class Event : ProtocolMessage
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06006FCC RID: 28620 RVA: 0x0004BFC0 File Offset: 0x0004A1C0
		// (set) Token: 0x06006FCD RID: 28621 RVA: 0x0004BFC8 File Offset: 0x0004A1C8
		public string @event { get; private set; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06006FCE RID: 28622 RVA: 0x0004BFD1 File Offset: 0x0004A1D1
		// (set) Token: 0x06006FCF RID: 28623 RVA: 0x0004BFD9 File Offset: 0x0004A1D9
		public object body { get; private set; }

		// Token: 0x06006FD0 RID: 28624 RVA: 0x0004BFE2 File Offset: 0x0004A1E2
		public Event(string type, object bdy = null) : base("event")
		{
			this.@event = type;
			this.body = bdy;
		}
	}
}
