using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D94 RID: 3476
	public class Message
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060062FF RID: 25343 RVA: 0x0027A48B File Offset: 0x0027868B
		// (set) Token: 0x06006300 RID: 25344 RVA: 0x0027A493 File Offset: 0x00278693
		public int id { get; private set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x0027A49C File Offset: 0x0027869C
		// (set) Token: 0x06006302 RID: 25346 RVA: 0x0027A4A4 File Offset: 0x002786A4
		public string format { get; private set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x0027A4AD File Offset: 0x002786AD
		// (set) Token: 0x06006304 RID: 25348 RVA: 0x0027A4B5 File Offset: 0x002786B5
		public object variables { get; private set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x0027A4BE File Offset: 0x002786BE
		// (set) Token: 0x06006306 RID: 25350 RVA: 0x0027A4C6 File Offset: 0x002786C6
		public object showUser { get; private set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06006307 RID: 25351 RVA: 0x0027A4CF File Offset: 0x002786CF
		// (set) Token: 0x06006308 RID: 25352 RVA: 0x0027A4D7 File Offset: 0x002786D7
		public object sendTelemetry { get; private set; }

		// Token: 0x06006309 RID: 25353 RVA: 0x0027A4E0 File Offset: 0x002786E0
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
