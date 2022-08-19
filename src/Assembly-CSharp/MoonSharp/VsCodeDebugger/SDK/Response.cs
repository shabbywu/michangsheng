using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DAD RID: 3501
	public class Response : ProtocolMessage
	{
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06006379 RID: 25465 RVA: 0x0027AFB6 File Offset: 0x002791B6
		// (set) Token: 0x0600637A RID: 25466 RVA: 0x0027AFBE File Offset: 0x002791BE
		public bool success { get; private set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600637B RID: 25467 RVA: 0x0027AFC7 File Offset: 0x002791C7
		// (set) Token: 0x0600637C RID: 25468 RVA: 0x0027AFCF File Offset: 0x002791CF
		public string message { get; private set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600637D RID: 25469 RVA: 0x0027AFD8 File Offset: 0x002791D8
		// (set) Token: 0x0600637E RID: 25470 RVA: 0x0027AFE0 File Offset: 0x002791E0
		public int request_seq { get; private set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x0027AFE9 File Offset: 0x002791E9
		// (set) Token: 0x06006380 RID: 25472 RVA: 0x0027AFF1 File Offset: 0x002791F1
		public string command { get; private set; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x0027AFFA File Offset: 0x002791FA
		// (set) Token: 0x06006382 RID: 25474 RVA: 0x0027B002 File Offset: 0x00279202
		public ResponseBody body { get; private set; }

		// Token: 0x06006383 RID: 25475 RVA: 0x0027B00B File Offset: 0x0027920B
		public Response(Table req) : base("response")
		{
			this.success = true;
			this.request_seq = req.Get("seq").ToObject<int>();
			this.command = req.Get("command").ToObject<string>();
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x0027B04B File Offset: 0x0027924B
		public void SetBody(ResponseBody bdy)
		{
			this.success = true;
			this.body = bdy;
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0027B05B File Offset: 0x0027925B
		public void SetErrorBody(string msg, ResponseBody bdy = null)
		{
			this.success = false;
			this.message = msg;
			this.body = bdy;
		}
	}
}
