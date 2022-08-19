using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x02000DB3 RID: 3507
	internal class EmptyDebugSession : DebugSession
	{
		// Token: 0x060063C3 RID: 25539 RVA: 0x0027BC66 File Offset: 0x00279E66
		internal EmptyDebugSession(MoonSharpVsCodeDebugServer server) : base(true, false)
		{
			this.m_Server = server;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0027BC78 File Offset: 0x00279E78
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

		// Token: 0x060063C5 RID: 25541 RVA: 0x0027BD24 File Offset: 0x00279F24
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

		// Token: 0x060063C6 RID: 25542 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void Attach(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0027BE22 File Offset: 0x0027A022
		public override void Continue(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void Disconnect(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0027BE34 File Offset: 0x0027A034
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

		// Token: 0x060063CA RID: 25546 RVA: 0x0027BE68 File Offset: 0x0027A068
		public override void Evaluate(Response response, Table args)
		{
			string @string = EmptyDebugSession.getString(args, "expression", null);
			if ((EmptyDebugSession.getString(args, "context", null) ?? "hover") == "repl")
			{
				this.ExecuteRepl(@string);
			}
			base.SendResponse(response, null);
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0027BEB4 File Offset: 0x0027A0B4
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

		// Token: 0x060063CC RID: 25548 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void Launch(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0027BE22 File Offset: 0x0027A022
		public override void Next(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x0027BE22 File Offset: 0x0027A022
		public override void Pause(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void Scopes(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void SetBreakpoints(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void StackTrace(Response response, Table args)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x0027BE22 File Offset: 0x0027A022
		public override void StepIn(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x0027BE22 File Offset: 0x0027A022
		public override void StepOut(Response response, Table arguments)
		{
			this.SendList();
			base.SendResponse(response, null);
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0027BEFC File Offset: 0x0027A0FC
		public override void Threads(Response response, Table arguments)
		{
			List<Thread> vars = new List<Thread>
			{
				new Thread(0, "Main Thread")
			};
			base.SendResponse(response, new ThreadsResponseBody(vars));
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x0027BE18 File Offset: 0x0027A018
		public override void Variables(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0027BF2D File Offset: 0x0027A12D
		private void SendText(string msg, params object[] args)
		{
			msg = string.Format(msg, args);
			base.SendEvent(new OutputEvent("console", msg + "\n"));
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x0027BF53 File Offset: 0x0027A153
		public void Unbind()
		{
			this.SendText("Bye.", Array.Empty<object>());
			base.SendEvent(new TerminatedEvent());
		}

		// Token: 0x040055FF RID: 22015
		private MoonSharpVsCodeDebugServer m_Server;
	}
}
