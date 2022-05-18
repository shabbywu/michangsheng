using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x020011E3 RID: 4579
	internal class MoonSharpDebugSession : DebugSession, IAsyncDebuggerClient
	{
		// Token: 0x06007028 RID: 28712 RVA: 0x0004C275 File Offset: 0x0004A475
		internal MoonSharpDebugSession(MoonSharpVsCodeDebugServer server, AsyncDebugger debugger) : base(true, false)
		{
			this.m_Server = server;
			this.m_Debug = debugger;
		}

		// Token: 0x06007029 RID: 28713 RVA: 0x002A13F4 File Offset: 0x0029F5F4
		public override void Initialize(Response response, Table args)
		{
			this.SendText("Connected to MoonSharp {0} [{1}] on process {2} (PID {3})", new object[]
			{
				"2.0.0.0",
				Script.GlobalOptions.Platform.GetPlatformName(),
				Process.GetCurrentProcess().ProcessName,
				Process.GetCurrentProcess().Id
			});
			this.SendText("Debugging script '{0}'; use the debug console to debug another script.", new object[]
			{
				this.m_Debug.Name
			});
			this.SendText("Type '!help' in the Debug Console for available commands.", Array.Empty<object>());
			base.SendResponse(response, new Capabilities
			{
				supportsConfigurationDoneRequest = false,
				supportsFunctionBreakpoints = false,
				supportsConditionalBreakpoints = false,
				supportsEvaluateForHovers = false,
				exceptionBreakpointFilters = new object[0]
			});
			base.SendEvent(new InitializedEvent());
			this.m_Debug.Client = this;
		}

		// Token: 0x0600702A RID: 28714 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Attach(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x0004C2A9 File Offset: 0x0004A4A9
		public override void Continue(Response response, Table arguments)
		{
			this.m_Debug.QueueAction(new DebuggerAction
			{
				Action = DebuggerAction.ActionType.Run
			});
			base.SendResponse(response, null);
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x0004C2CA File Offset: 0x0004A4CA
		public override void Disconnect(Response response, Table arguments)
		{
			this.m_Debug.Client = null;
			base.SendResponse(response, null);
		}

		// Token: 0x0600702D RID: 28717 RVA: 0x002A12F8 File Offset: 0x0029F4F8
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

		// Token: 0x0600702E RID: 28718 RVA: 0x002A14C8 File Offset: 0x0029F6C8
		public override void Evaluate(Response response, Table args)
		{
			string @string = MoonSharpDebugSession.getString(args, "expression", null);
			bool @int = this.getInt(args, "frameId", 0) != 0;
			string a = MoonSharpDebugSession.getString(args, "context", null) ?? "hover";
			if (@int && a != "repl")
			{
				this.SendText("Warning : Evaluation of variables/watches is always done with the top-level scope.", Array.Empty<object>());
			}
			if (a == "repl" && @string.StartsWith("!"))
			{
				this.ExecuteRepl(@string.Substring(1));
				base.SendResponse(response, null);
				return;
			}
			DynValue dynValue = this.m_Debug.Evaluate(@string) ?? DynValue.Nil;
			this.m_Variables.Add(dynValue);
			base.SendResponse(response, new EvaluateResponseBody(dynValue.ToDebugPrintString(), this.m_Variables.Count - 1)
			{
				type = dynValue.Type.ToLuaDebuggerString()
			});
		}

		// Token: 0x0600702F RID: 28719 RVA: 0x002A15A8 File Offset: 0x0029F7A8
		private void ExecuteRepl(string cmd)
		{
			bool flag = false;
			cmd = cmd.Trim();
			if (cmd == "help")
			{
				flag = true;
			}
			else if (cmd.StartsWith("geterror"))
			{
				this.SendText("Current error regex : {0}", new object[]
				{
					this.m_Debug.ErrorRegex.ToString()
				});
			}
			else
			{
				if (cmd.StartsWith("seterror"))
				{
					string pattern = cmd.Substring("seterror".Length).Trim();
					try
					{
						Regex errorRegex = new Regex(pattern);
						this.m_Debug.ErrorRegex = errorRegex;
						this.SendText("Current error regex : {0}", new object[]
						{
							this.m_Debug.ErrorRegex.ToString()
						});
						goto IL_313;
					}
					catch (Exception ex)
					{
						this.SendText("Error setting regex: {0}", new object[]
						{
							ex.Message
						});
						goto IL_313;
					}
				}
				if (cmd.StartsWith("execendnotify"))
				{
					string text = cmd.Substring("execendnotify".Length).Trim();
					if (text == "off")
					{
						this.m_NotifyExecutionEnd = false;
					}
					else if (text == "on")
					{
						this.m_NotifyExecutionEnd = true;
					}
					else if (text.Length > 0)
					{
						this.SendText("Error : expected 'on' or 'off'", Array.Empty<object>());
					}
					this.SendText("Notifications of execution end are : {0}", new object[]
					{
						this.m_NotifyExecutionEnd ? "enabled" : "disabled"
					});
				}
				else
				{
					if (cmd == "list")
					{
						int num = this.m_Server.CurrentId ?? -1000;
						using (IEnumerator<KeyValuePair<int, string>> enumerator = this.m_Server.GetAttachedDebuggersByIdAndName().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, string> keyValuePair = enumerator.Current;
								string text2 = (keyValuePair.Key == this.m_Debug.Id) ? " (this)" : "";
								string text3 = (keyValuePair.Key == num) ? " (default)" : "";
								this.SendText("{0} : {1}{2}{3}", new object[]
								{
									keyValuePair.Key.ToString().PadLeft(9),
									keyValuePair.Value,
									text3,
									text2
								});
							}
							goto IL_313;
						}
					}
					if (cmd.StartsWith("select") || cmd.StartsWith("switch"))
					{
						string s = cmd.Substring("switch".Length).Trim();
						try
						{
							int num2 = int.Parse(s);
							this.m_Server.CurrentId = new int?(num2);
							if (cmd.StartsWith("switch"))
							{
								this.Unbind();
							}
							else
							{
								this.SendText("Next time you'll attach the debugger, it will be atteched to script #{0}", new object[]
								{
									num2
								});
							}
							goto IL_313;
						}
						catch (Exception ex2)
						{
							this.SendText("Error setting regex: {0}", new object[]
							{
								ex2.Message
							});
							goto IL_313;
						}
					}
					this.SendText("Syntax error : {0}\n", new object[]
					{
						cmd
					});
					flag = true;
				}
			}
			IL_313:
			if (flag)
			{
				this.SendText("Available commands : ", Array.Empty<object>());
				this.SendText("    !help - gets this help", Array.Empty<object>());
				this.SendText("    !list - lists the other scripts which can be debugged", Array.Empty<object>());
				this.SendText("    !select <id> - select another script for future sessions", Array.Empty<object>());
				this.SendText("    !switch <id> - switch to another script (same as select + disconnect)", Array.Empty<object>());
				this.SendText("    !seterror <regex> - sets the regex which tells which errors to trap", Array.Empty<object>());
				this.SendText("    !geterror - gets the current value of the regex which tells which errors to trap", Array.Empty<object>());
				this.SendText("    !execendnotify [on|off] - sets the notification of end of execution on or off (default = off)", Array.Empty<object>());
				this.SendText("    ... or type an expression to evaluate it on the fly.", Array.Empty<object>());
			}
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x0004C218 File Offset: 0x0004A418
		public override void Launch(Response response, Table arguments)
		{
			base.SendResponse(response, null);
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x0004C2E0 File Offset: 0x0004A4E0
		public override void Next(Response response, Table arguments)
		{
			this.m_Debug.QueueAction(new DebuggerAction
			{
				Action = DebuggerAction.ActionType.StepOver
			});
			base.SendResponse(response, null);
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x0004C301 File Offset: 0x0004A501
		private StoppedEvent CreateStoppedEvent(string reason, string text = null)
		{
			return new StoppedEvent(0, reason, text);
		}

		// Token: 0x06007033 RID: 28723 RVA: 0x0004C30B File Offset: 0x0004A50B
		public override void Pause(Response response, Table arguments)
		{
			this.m_Debug.PauseRequested = true;
			base.SendResponse(response, null);
			this.SendText("Pause pending -- will pause at first script statement.", Array.Empty<object>());
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x002A1988 File Offset: 0x0029FB88
		public override void Scopes(Response response, Table arguments)
		{
			base.SendResponse(response, new ScopesResponseBody(new List<Scope>
			{
				new Scope("Locals", 65536, false),
				new Scope("Self", 65537, false)
			}));
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x002A19D4 File Offset: 0x0029FBD4
		public override void SetBreakpoints(Response response, Table args)
		{
			string text = null;
			Table table = args["source"] as Table;
			if (table != null)
			{
				string text2 = table["path"].ToString();
				if (text2 != null && text2.Trim().Length > 0)
				{
					text = text2;
				}
			}
			if (text == null)
			{
				base.SendErrorResponse(response, 3010, "setBreakpoints: property 'source' is empty or misformed", null, false, true);
				return;
			}
			text = base.ConvertClientPathToDebugger(text);
			SourceCode sourceCode = this.m_Debug.FindSourceByName(text);
			if (sourceCode == null)
			{
				base.SendResponse(response, new SetBreakpointsResponseBody(null));
				return;
			}
			HashSet<int> hashSet = new HashSet<int>((from jt in args.Get("lines").Table.Values
			select base.ConvertClientLineToDebugger(jt.ToObject<int>())).ToArray<int>());
			HashSet<int> hashSet2 = this.m_Debug.DebugService.ResetBreakPoints(sourceCode, hashSet);
			List<Breakpoint> list = new List<Breakpoint>();
			foreach (int num in hashSet)
			{
				list.Add(new Breakpoint(hashSet2.Contains(num), num));
			}
			response.SetBody(new SetBreakpointsResponseBody(list));
			base.SendResponse(response, null);
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x002A1B14 File Offset: 0x0029FD14
		public override void StackTrace(Response response, Table args)
		{
			int @int = this.getInt(args, "levels", 10);
			List<MoonSharp.VsCodeDebugger.SDK.StackFrame> list = new List<MoonSharp.VsCodeDebugger.SDK.StackFrame>();
			List<WatchItem> watches = this.m_Debug.GetWatches(WatchType.CallStack);
			WatchItem watchItem = this.m_Debug.GetWatches(WatchType.Threads).LastOrDefault<WatchItem>();
			int i = 0;
			int num = Math.Min(@int - 3, watches.Count);
			while (i < num)
			{
				WatchItem watchItem2 = watches[i];
				string name = watchItem2.Name;
				SourceRef sourceRef = watchItem2.Location ?? this.DefaultSourceRef;
				int sourceIdx = sourceRef.SourceIdx;
				string path = sourceRef.IsClrLocation ? "(native)" : (this.m_Debug.GetSourceFile(sourceIdx) ?? "???");
				Source source = new Source(Path.GetFileName(path), path, 0);
				list.Add(new MoonSharp.VsCodeDebugger.SDK.StackFrame(i, name, source, base.ConvertDebuggerLineToClient(sourceRef.FromLine), sourceRef.FromChar, new int?(base.ConvertDebuggerLineToClient(sourceRef.ToLine)), new int?(sourceRef.ToChar)));
				i++;
			}
			if (watches.Count > @int - 3)
			{
				list.Add(new MoonSharp.VsCodeDebugger.SDK.StackFrame(i++, "(...)", null, 0, 0, null, null));
			}
			if (watchItem != null)
			{
				list.Add(new MoonSharp.VsCodeDebugger.SDK.StackFrame(i++, "(" + watchItem.Name + ")", null, 0, 0, null, null));
			}
			else
			{
				list.Add(new MoonSharp.VsCodeDebugger.SDK.StackFrame(i++, "(main coroutine)", null, 0, 0, null, null));
			}
			list.Add(new MoonSharp.VsCodeDebugger.SDK.StackFrame(i++, "(native)", null, 0, 0, null, null));
			base.SendResponse(response, new StackTraceResponseBody(list));
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x002A1D08 File Offset: 0x0029FF08
		private int getInt(Table args, string propName, int defaultValue)
		{
			DynValue dynValue = args.Get(propName);
			if (dynValue.Type != DataType.Number)
			{
				return defaultValue;
			}
			return dynValue.ToObject<int>();
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x0004C331 File Offset: 0x0004A531
		public override void StepIn(Response response, Table arguments)
		{
			this.m_Debug.QueueAction(new DebuggerAction
			{
				Action = DebuggerAction.ActionType.StepIn
			});
			base.SendResponse(response, null);
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x0004C352 File Offset: 0x0004A552
		public override void StepOut(Response response, Table arguments)
		{
			this.m_Debug.QueueAction(new DebuggerAction
			{
				Action = DebuggerAction.ActionType.StepOut
			});
			base.SendResponse(response, null);
		}

		// Token: 0x0600703A RID: 28730 RVA: 0x002A13C0 File Offset: 0x0029F5C0
		public override void Threads(Response response, Table arguments)
		{
			List<Thread> vars = new List<Thread>
			{
				new Thread(0, "Main Thread")
			};
			base.SendResponse(response, new ThreadsResponseBody(vars));
		}

		// Token: 0x0600703B RID: 28731 RVA: 0x002A1D30 File Offset: 0x0029FF30
		public override void Variables(Response response, Table arguments)
		{
			int @int = this.getInt(arguments, "variablesReference", -1);
			List<Variable> list = new List<Variable>();
			if (@int == 65537)
			{
				VariableInspector.InspectVariable(this.m_Debug.Evaluate("self"), list);
			}
			else
			{
				if (@int == 65536)
				{
					using (List<WatchItem>.Enumerator enumerator = this.m_Debug.GetWatches(WatchType.Locals).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							WatchItem watchItem = enumerator.Current;
							list.Add(new Variable(watchItem.Name, (watchItem.Value ?? DynValue.Void).ToDebugPrintString(), 0));
						}
						goto IL_D2;
					}
				}
				if (@int < 0 || @int >= this.m_Variables.Count)
				{
					list.Add(new Variable("<error>", null, 0));
				}
				else
				{
					VariableInspector.InspectVariable(this.m_Variables[@int], list);
				}
			}
			IL_D2:
			base.SendResponse(response, new VariablesResponseBody(list));
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x0004C373 File Offset: 0x0004A573
		void IAsyncDebuggerClient.SendStopEvent()
		{
			base.SendEvent(this.CreateStoppedEvent("step", null));
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x0004C387 File Offset: 0x0004A587
		void IAsyncDebuggerClient.OnWatchesUpdated(WatchType watchType)
		{
			if (watchType == WatchType.CallStack)
			{
				this.m_Variables.Clear();
			}
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x002A1E2C File Offset: 0x002A002C
		void IAsyncDebuggerClient.OnSourceCodeChanged(int sourceID)
		{
			if (this.m_Debug.IsSourceOverride(sourceID))
			{
				this.SendText("Loaded source '{0}' -> '{1}'", new object[]
				{
					this.m_Debug.GetSource(sourceID).Name,
					this.m_Debug.GetSourceFile(sourceID)
				});
				return;
			}
			this.SendText("Loaded source '{0}'", new object[]
			{
				this.m_Debug.GetSource(sourceID).Name
			});
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x0004C398 File Offset: 0x0004A598
		public void OnExecutionEnded()
		{
			if (this.m_NotifyExecutionEnd)
			{
				this.SendText("Execution ended.", Array.Empty<object>());
			}
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x0004C232 File Offset: 0x0004A432
		private void SendText(string msg, params object[] args)
		{
			msg = string.Format(msg, args);
			base.SendEvent(new OutputEvent("console", msg + "\n"));
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		public void OnException(ScriptRuntimeException ex)
		{
			this.SendText("runtime error : {0}", new object[]
			{
				ex.DecoratedMessage
			});
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x0004C3CE File Offset: 0x0004A5CE
		public void Unbind()
		{
			this.SendText("Debug session has been closed by the hosting process.", Array.Empty<object>());
			this.SendText("Bye.", Array.Empty<object>());
			base.SendEvent(new TerminatedEvent());
		}

		// Token: 0x040062EB RID: 25323
		private AsyncDebugger m_Debug;

		// Token: 0x040062EC RID: 25324
		private MoonSharpVsCodeDebugServer m_Server;

		// Token: 0x040062ED RID: 25325
		private List<DynValue> m_Variables = new List<DynValue>();

		// Token: 0x040062EE RID: 25326
		private bool m_NotifyExecutionEnd;

		// Token: 0x040062EF RID: 25327
		private const int SCOPE_LOCALS = 65536;

		// Token: 0x040062F0 RID: 25328
		private const int SCOPE_SELF = 65537;

		// Token: 0x040062F1 RID: 25329
		private readonly SourceRef DefaultSourceRef = new SourceRef(-1, 0, 0, 0, 0, false);
	}
}
