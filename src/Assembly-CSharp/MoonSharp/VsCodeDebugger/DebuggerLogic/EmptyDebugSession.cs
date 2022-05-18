using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x020011E1 RID: 4577
	internal class EmptyDebugSession : DebugSession
	{
		// Token: 0x0600700D RID: 28685 RVA: 0x0004C207 File Offset: 0x0004A407
		internal EmptyDebugSession(MoonSharpVsCodeDebugServer server) : base(true, false)
		{
			this.m_Server = server;
		}

		// Token: 0x0600700E RID: 28686 RVA: 0x002A1158 File Offset: 0x0029F358
		public override void Initialize(Response response, Table args)
		{
			this.SendText("Connected to MoonSharp {0} [{1}] on process {2} (PID {3})", new object[]
			{
				"2.0.0.0",
				Script.GlobalOptions.Platform.GetPlatformName(),
				Process.GetCurrentProcess().ProcessName,
				Process.GetCurrentProcess().Id
			});
			this.SendText("No script is set as default for debugging; use the debug console to select the script to debug.\n", Array.Empty<object>());
			this.SendList();
			base.SendResponse(response, new Capabilities
			{
				supportsConfigurationDoneRequest = false,
				supportsFunctionBreakpoints = false,
				supportsConditionalBreakpoints = false,
				supportsEvaluateForHovers = false,
				exceptionBreakpointFilters = new object[0]
			});
			base.SendEvent(new InitializedEvent());
		}

		// Token: 0x0600700F RID: 28687 RVA: 0x002A1204 File Offset: 0x0029F404
		private void SendList()
		{
			int num = this.m_Server.CurrentId ?? -1000;
			this.SendText("==========================================================", Array.Empty<object>());
			foreach (KeyValuePair<int, string> keyValuePair in this.m_Server.GetAttachedDebuggersByIdAndName())
			{
				string text = (keyValuePair.Key == num) ? " (default)" : "";
				this.SendText("{0} : {1}{2}", new object[]
				{
					keyValuePair.Key.ToString().PadLeft(9),
					keyValuePair.Value,
					text
				});
			}
			this.SendText("", Array.Empty<object>());
			this.SendText("Type the number of the script to debug, or '!' to refresh", Array.Empty<object>());
		}

		// Token: 0x06007010 RID: 28688 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Attach(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06007011 RID: 28689 RVA: 0x0004C222 File Offset: 0x0004A422
		public override void Continue(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06007012 RID: 28690 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Disconnect(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06007013 RID: 28691 RVA: 0x002A12F8 File Offset: 0x0029F4F8
		private static string getString(Table args, string property, string dflt = null)
		{
			string text = (string)args[property];
			if (text == null)
			{
				return dflt;
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				return dflt;
			}
			return text;
		}

		// Token: 0x06007014 RID: 28692 RVA: 0x002A132C File Offset: 0x0029F52C
		public override void Evaluate(Response response, Table args)
		{
			string @string = EmptyDebugSession.getString(args, "expression", null);
			if ((EmptyDebugSession.getString(args, "context", null) ?? "hover") == "repl")
			{
				this.ExecuteRepl(@string);
			}
			base.SendResponse(response, null);
		}

		// Token: 0x06007015 RID: 28693 RVA: 0x002A1378 File Offset: 0x0029F578
		private void ExecuteRepl(string cmd)
		{
			int value = 0;
			if (int.TryParse(cmd, out value))
			{
				this.m_Server.CurrentId = new int?(value);
				this.SendText("Re-attach the debugger to debug the selected script.", Array.Empty<object>());
				this.Unbind();
				return;
			}
			this.SendList();
		}

		// Token: 0x06007016 RID: 28694 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Launch(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x0004C222 File Offset: 0x0004A422
		public override void Next(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06007018 RID: 28696 RVA: 0x0004C222 File Offset: 0x0004A422
		public override void Pause(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x06007019 RID: 28697 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Scopes(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600701A RID: 28698 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void SetBreakpoints(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void StackTrace(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x0004C222 File Offset: 0x0004A422
		public override void StepIn(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x0004C222 File Offset: 0x0004A422
		public override void StepOut(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x002A13C0 File Offset: 0x0029F5C0
		public override void Threads(Response response, Table arguments)
		{
			List<Thread> vars = new List<Thread>
			{
				new Thread(0, "Main Thread")
			};
			base.SendResponse(response, new ThreadsResponseBody(vars));
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Variables(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x0004C232 File Offset: 0x0004A432
		private void SendText(string msg, params object[] args)
		{
			msg = string.Format(msg, args);
			base.SendEvent(new OutputEvent("console", msg + "\n"));
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x0004C258 File Offset: 0x0004A458
		public void Unbind()
		{
			this.SendText("Bye.", Array.Empty<object>());
			base.SendEvent(new TerminatedEvent());
		}

		// Token: 0x040062EA RID: 25322
		private MoonSharpVsCodeDebugServer m_Server;
	}
}
