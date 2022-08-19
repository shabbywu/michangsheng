using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DAB RID: 3499
	public class Request : ProtocolMessage
	{
		// Token: 0x06006377 RID: 25463 RVA: 0x0027AF9A File Offset: 0x0027919A
		public Request(int id, string cmd, Table arg) : base("request", id)
		{
			this.command = cmd;
			this.arguments = arg;
		}

		// Token: 0x040055DA RID: 21978
		public string command;

		// Token: 0x040055DB RID: 21979
		public Table arguments;
	}
}
