using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DAE RID: 3502
	public class Event : ProtocolMessage
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06006386 RID: 25478 RVA: 0x0027B072 File Offset: 0x00279272
		// (set) Token: 0x06006387 RID: 25479 RVA: 0x0027B07A File Offset: 0x0027927A
		public string @event { get; private set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x0027B083 File Offset: 0x00279283
		// (set) Token: 0x06006389 RID: 25481 RVA: 0x0027B08B File Offset: 0x0027928B
		public object body { get; private set; }

		// Token: 0x0600638A RID: 25482 RVA: 0x0027B094 File Offset: 0x00279294
		public Event(string type, object bdy = null) : base("event")
		{
			this.@event = type;
			this.body = bdy;
		}
	}
}
