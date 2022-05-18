using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D5 RID: 4565
	public abstract class DebugSession : ProtocolServer
	{
		// Token: 0x06006F9F RID: 28575 RVA: 0x0004BE1B File Offset: 0x0004A01B
		public DebugSession(bool debuggerLinesStartAt1, bool debuggerPathsAreURI = false)
		{
			this._debuggerLinesStartAt1 = debuggerLinesStartAt1;
			this._debuggerPathsAreURI = debuggerPathsAreURI;
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x0004BE3F File Offset: 0x0004A03F
		public void SendResponse(Response response, ResponseBody body = null)
		{
			if (body != null)
			{
				response.SetBody(body);
			}
			base.SendMessage(response);
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x002A0194 File Offset: 0x0029E394
		public void SendErrorResponse(Response response, int id, string format, object arguments = null, bool user = true, bool telemetry = false)
		{
			Message message = new Message(id, format, arguments, user, telemetry);
			string msg = Utilities.ExpandVariables(message.format, message.variables, true);
			response.SetErrorBody(msg, new ErrorResponseBody(message));
			base.SendMessage(response);
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x002A01D8 File Offset: 0x0029E3D8
		protected override void DispatchRequest(string command, Table args, Response response)
		{
			if (args == null)
			{
				args = new Table(null);
			}
			try
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(command);
				if (num <= 1531507585U)
				{
					if (num <= 466561496U)
					{
						if (num <= 215793505U)
						{
							if (num != 197563686U)
							{
								if (num == 215793505U)
								{
									if (command == "setFunctionBreakpoints")
									{
										this.SetFunctionBreakpoints(response, args);
										goto IL_415;
									}
								}
							}
							else if (command == "threads")
							{
								this.Threads(response, args);
								goto IL_415;
							}
						}
						else if (num != 362146064U)
						{
							if (num == 466561496U)
							{
								if (command == "source")
								{
									this.Source(response, args);
									goto IL_415;
								}
							}
						}
						else if (command == "setExceptionBreakpoints")
						{
							this.SetExceptionBreakpoints(response, args);
							goto IL_415;
						}
					}
					else if (num <= 699097794U)
					{
						if (num != 515025409U)
						{
							if (num == 699097794U)
							{
								if (command == "variables")
								{
									this.Variables(response, args);
									goto IL_415;
								}
							}
						}
						else if (command == "stepOut")
						{
							this.StepOut(response, args);
							goto IL_415;
						}
					}
					else if (num != 1117107268U)
					{
						if (num != 1172766443U)
						{
							if (num == 1531507585U)
							{
								if (command == "setBreakpoints")
								{
									this.SetBreakpoints(response, args);
									goto IL_415;
								}
							}
						}
						else if (command == "disconnect")
						{
							this.Disconnect(response, args);
							goto IL_415;
						}
					}
					else if (command == "attach")
					{
						this.Attach(response, args);
						goto IL_415;
					}
				}
				else if (num <= 2977070660U)
				{
					if (num <= 1887753101U)
					{
						if (num != 1555467752U)
						{
							if (num == 1887753101U)
							{
								if (command == "pause")
								{
									this.Pause(response, args);
									goto IL_415;
								}
							}
						}
						else if (command == "next")
						{
							this.Next(response, args);
							goto IL_415;
						}
					}
					else if (num != 2900637194U)
					{
						if (num == 2977070660U)
						{
							if (command == "continue")
							{
								this.Continue(response, args);
								goto IL_415;
							}
						}
					}
					else if (command == "evaluate")
					{
						this.Evaluate(response, args);
						goto IL_415;
					}
				}
				else if (num <= 3751489852U)
				{
					if (num != 3465713227U)
					{
						if (num == 3751489852U)
						{
							if (command == "stackTrace")
							{
								this.StackTrace(response, args);
								goto IL_415;
							}
						}
					}
					else if (command == "initialize")
					{
						if (args["linesStartAt1"] != null)
						{
							this._clientLinesStartAt1 = args.Get("linesStartAt1").ToObject<bool>();
						}
						string text = args.Get("pathFormat").ToObject<string>();
						if (text != null)
						{
							if (!(text == "uri"))
							{
								if (!(text == "path"))
								{
									this.SendErrorResponse(response, 1015, "initialize: bad value '{_format}' for pathFormat", new
									{
										_format = text
									}, true, false);
									return;
								}
								this._clientPathsAreURI = false;
							}
							else
							{
								this._clientPathsAreURI = true;
							}
						}
						this.Initialize(response, args);
						goto IL_415;
					}
				}
				else if (num != 4055382118U)
				{
					if (num != 4060112084U)
					{
						if (num == 4120737864U)
						{
							if (command == "scopes")
							{
								this.Scopes(response, args);
								goto IL_415;
							}
						}
					}
					else if (command == "stepIn")
					{
						this.StepIn(response, args);
						goto IL_415;
					}
				}
				else if (command == "launch")
				{
					this.Launch(response, args);
					goto IL_415;
				}
				this.SendErrorResponse(response, 1014, "unrecognized request: {_request}", new
				{
					_request = command
				}, true, false);
				IL_415:;
			}
			catch (Exception ex)
			{
				this.SendErrorResponse(response, 1104, "error while processing request '{_request}' (exception: {_exception})", new
				{
					_request = command,
					_exception = ex.Message
				}, true, false);
			}
			if (command == "disconnect")
			{
				base.Stop();
			}
		}

		// Token: 0x06006FA3 RID: 28579
		public abstract void Initialize(Response response, Table args);

		// Token: 0x06006FA4 RID: 28580
		public abstract void Launch(Response response, Table arguments);

		// Token: 0x06006FA5 RID: 28581
		public abstract void Attach(Response response, Table arguments);

		// Token: 0x06006FA6 RID: 28582
		public abstract void Disconnect(Response response, Table arguments);

		// Token: 0x06006FA7 RID: 28583 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void SetFunctionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void SetExceptionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06006FA9 RID: 28585
		public abstract void SetBreakpoints(Response response, Table arguments);

		// Token: 0x06006FAA RID: 28586
		public abstract void Continue(Response response, Table arguments);

		// Token: 0x06006FAB RID: 28587
		public abstract void Next(Response response, Table arguments);

		// Token: 0x06006FAC RID: 28588
		public abstract void StepIn(Response response, Table arguments);

		// Token: 0x06006FAD RID: 28589
		public abstract void StepOut(Response response, Table arguments);

		// Token: 0x06006FAE RID: 28590
		public abstract void Pause(Response response, Table arguments);

		// Token: 0x06006FAF RID: 28591
		public abstract void StackTrace(Response response, Table arguments);

		// Token: 0x06006FB0 RID: 28592
		public abstract void Scopes(Response response, Table arguments);

		// Token: 0x06006FB1 RID: 28593
		public abstract void Variables(Response response, Table arguments);

		// Token: 0x06006FB2 RID: 28594 RVA: 0x0004BE52 File Offset: 0x0004A052
		public virtual void Source(Response response, Table arguments)
		{
			this.SendErrorResponse(response, 1020, "Source not supported", null, true, false);
		}

		// Token: 0x06006FB3 RID: 28595
		public abstract void Threads(Response response, Table arguments);

		// Token: 0x06006FB4 RID: 28596
		public abstract void Evaluate(Response response, Table arguments);

		// Token: 0x06006FB5 RID: 28597 RVA: 0x0004BE68 File Offset: 0x0004A068
		protected int ConvertDebuggerLineToClient(int line)
		{
			if (this._debuggerLinesStartAt1)
			{
				if (!this._clientLinesStartAt1)
				{
					return line - 1;
				}
				return line;
			}
			else
			{
				if (!this._clientLinesStartAt1)
				{
					return line;
				}
				return line + 1;
			}
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x0004BE8D File Offset: 0x0004A08D
		protected int ConvertClientLineToDebugger(int line)
		{
			if (this._debuggerLinesStartAt1)
			{
				if (!this._clientLinesStartAt1)
				{
					return line + 1;
				}
				return line;
			}
			else
			{
				if (!this._clientLinesStartAt1)
				{
					return line;
				}
				return line - 1;
			}
		}

		// Token: 0x06006FB7 RID: 28599 RVA: 0x002A0650 File Offset: 0x0029E850
		protected string ConvertDebuggerPathToClient(string path)
		{
			if (!this._debuggerPathsAreURI)
			{
				if (this._clientPathsAreURI)
				{
					try
					{
						return new Uri(path).AbsoluteUri;
					}
					catch
					{
						return null;
					}
					return path;
				}
				return path;
			}
			if (this._clientPathsAreURI)
			{
				return path;
			}
			return new Uri(path).LocalPath;
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x002A06AC File Offset: 0x0029E8AC
		protected string ConvertClientPathToDebugger(string clientPath)
		{
			if (clientPath == null)
			{
				return null;
			}
			if (this._debuggerPathsAreURI)
			{
				if (this._clientPathsAreURI)
				{
					return clientPath;
				}
				return new Uri(clientPath).AbsoluteUri;
			}
			else
			{
				if (!this._clientPathsAreURI)
				{
					return clientPath;
				}
				if (Uri.IsWellFormedUriString(clientPath, UriKind.Absolute))
				{
					return new Uri(clientPath).LocalPath;
				}
				Console.Error.WriteLine("path not well formed: '{0}'", clientPath);
				return null;
			}
		}

		// Token: 0x040062BB RID: 25275
		private bool _debuggerLinesStartAt1;

		// Token: 0x040062BC RID: 25276
		private bool _debuggerPathsAreURI;

		// Token: 0x040062BD RID: 25277
		private bool _clientLinesStartAt1 = true;

		// Token: 0x040062BE RID: 25278
		private bool _clientPathsAreURI = true;
	}
}
