using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D3 RID: 4563
	public class EvaluateResponseBody : ResponseBody
	{
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06006F95 RID: 28565 RVA: 0x0004BD9D File Offset: 0x00049F9D
		// (set) Token: 0x06006F96 RID: 28566 RVA: 0x0004BDA5 File Offset: 0x00049FA5
		public string result { get; private set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0004BDAE File Offset: 0x00049FAE
		// (set) Token: 0x06006F98 RID: 28568 RVA: 0x0004BDB6 File Offset: 0x00049FB6
		public string type { get; set; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x0004BDBF File Offset: 0x00049FBF
		// (set) Token: 0x06006F9A RID: 28570 RVA: 0x0004BDC7 File Offset: 0x00049FC7
		public int variablesReference { get; private set; }

		// Token: 0x06006F9B RID: 28571 RVA: 0x0004BDD0 File Offset: 0x00049FD0
		public EvaluateResponseBody(string value, int reff = 0)
		{
			this.result = value;
			this.variablesReference = reff;
		}
	}
}
