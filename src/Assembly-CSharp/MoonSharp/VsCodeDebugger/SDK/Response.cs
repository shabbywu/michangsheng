using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D9 RID: 4569
	public class Response : ProtocolMessage
	{
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06006FBF RID: 28607 RVA: 0x0004BF04 File Offset: 0x0004A104
		// (set) Token: 0x06006FC0 RID: 28608 RVA: 0x0004BF0C File Offset: 0x0004A10C
		public bool success { get; private set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x0004BF15 File Offset: 0x0004A115
		// (set) Token: 0x06006FC2 RID: 28610 RVA: 0x0004BF1D File Offset: 0x0004A11D
		public string message { get; private set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06006FC3 RID: 28611 RVA: 0x0004BF26 File Offset: 0x0004A126
		// (set) Token: 0x06006FC4 RID: 28612 RVA: 0x0004BF2E File Offset: 0x0004A12E
		public int request_seq { get; private set; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06006FC5 RID: 28613 RVA: 0x0004BF37 File Offset: 0x0004A137
		// (set) Token: 0x06006FC6 RID: 28614 RVA: 0x0004BF3F File Offset: 0x0004A13F
		public string command { get; private set; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06006FC7 RID: 28615 RVA: 0x0004BF48 File Offset: 0x0004A148
		// (set) Token: 0x06006FC8 RID: 28616 RVA: 0x0004BF50 File Offset: 0x0004A150
		public ResponseBody body { get; private set; }

		// Token: 0x06006FC9 RID: 28617 RVA: 0x0004BF59 File Offset: 0x0004A159
		public Response(Table req) : base("response")
		{
			this.success = true;
			this.request_seq = req.Get("seq").ToObject<int>();
			this.command = req.Get("command").ToObject<string>();
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x0004BF99 File Offset: 0x0004A199
		public void SetBody(ResponseBody bdy)
		{
			this.success = true;
			this.body = bdy;
		}

		// Token: 0x06006FCB RID: 28619 RVA: 0x0004BFA9 File Offset: 0x0004A1A9
		public void SetErrorBody(string msg, ResponseBody bdy = null)
		{
			this.success = false;
			this.message = msg;
			this.body = bdy;
		}
	}
}
