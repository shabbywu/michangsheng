using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D7 RID: 4567
	public class Request : ProtocolMessage
	{
		// Token: 0x06006FBD RID: 28605 RVA: 0x0004BEE8 File Offset: 0x0004A0E8
		public Request(int id, string cmd, Table arg) : base("request", id)
		{
			this.command = cmd;
			this.arguments = arg;
		}

		// Token: 0x040062C1 RID: 25281
		public string command;

		// Token: 0x040062C2 RID: 25282
		public Table arguments;
	}
}
