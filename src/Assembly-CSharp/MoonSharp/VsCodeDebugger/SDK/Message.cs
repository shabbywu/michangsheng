using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C0 RID: 4544
	public class Message
	{
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06006F45 RID: 28485 RVA: 0x0004B951 File Offset: 0x00049B51
		// (set) Token: 0x06006F46 RID: 28486 RVA: 0x0004B959 File Offset: 0x00049B59
		public int id { get; private set; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06006F47 RID: 28487 RVA: 0x0004B962 File Offset: 0x00049B62
		// (set) Token: 0x06006F48 RID: 28488 RVA: 0x0004B96A File Offset: 0x00049B6A
		public string format { get; private set; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06006F49 RID: 28489 RVA: 0x0004B973 File Offset: 0x00049B73
		// (set) Token: 0x06006F4A RID: 28490 RVA: 0x0004B97B File Offset: 0x00049B7B
		public object variables { get; private set; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06006F4B RID: 28491 RVA: 0x0004B984 File Offset: 0x00049B84
		// (set) Token: 0x06006F4C RID: 28492 RVA: 0x0004B98C File Offset: 0x00049B8C
		public object showUser { get; private set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06006F4D RID: 28493 RVA: 0x0004B995 File Offset: 0x00049B95
		// (set) Token: 0x06006F4E RID: 28494 RVA: 0x0004B99D File Offset: 0x00049B9D
		public object sendTelemetry { get; private set; }

		// Token: 0x06006F4F RID: 28495 RVA: 0x0004B9A6 File Offset: 0x00049BA6
		public Message(int id, string format, object variables = null, bool user = true, bool telemetry = false)
		{
			this.id = id;
			this.format = format;
			this.variables = variables;
			this.showUser = user;
			this.sendTelemetry = telemetry;
		}
	}
}
