using System;
using MoonSharp.Interpreter;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA9 RID: 3497
	public abstract class DebugSession : ProtocolServer
	{
		// Token: 0x06006359 RID: 25433 RVA: 0x0027A955 File Offset: 0x00278B55
		public DebugSession(bool debuggerLinesStartAt1, bool debuggerPathsAreURI = false)
		{
			this._debuggerLinesStartAt1 = debuggerLinesStartAt1;
			this._debuggerPathsAreURI = debuggerPathsAreURI;
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x0027A979 File Offset: 0x00278B79
		public void SendResponse(Response response, ResponseBody body = null)
		{
			if (body != null)
			{
				response.SetBody(body);
			}
			base.SendMessage(response);
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x0027A98C File Offset: 0x00278B8C
		public void SendErrorResponse(Response response, int id, string format, object arguments = null, bool user = true, bool telemetry = false)
		{
			Message message = new Message(id, format, arguments, user, telemetry);
			string msg = Utilities.ExpandVariables(message.format, message.variables, true);
			response.SetErrorBody(msg, new ErrorResponseBody(message));
			base.SendMessage(response);
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0027A9D0 File Offset: 0x00278BD0
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

		// Token: 0x0600635D RID: 25437
		public abstract void Initialize(Response response, Table args);

		// Token: 0x0600635E RID: 25438
		public abstract void Launch(Response response, Table arguments);

		// Token: 0x0600635F RID: 25439
		public abstract void Attach(Response response, Table arguments);

		// Token: 0x06006360 RID: 25440
		public abstract void Disconnect(Response response, Table arguments);

		// Token: 0x06006361 RID: 25441 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void SetFunctionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void SetExceptionBreakpoints(Response response, Table arguments)
		{
		}

		// Token: 0x06006363 RID: 25443
		public abstract void SetBreakpoints(Response response, Table arguments);

		// Token: 0x06006364 RID: 25444
		public abstract void Continue(Response response, Table arguments);

		// Token: 0x06006365 RID: 25445
		public abstract void Next(Response response, Table arguments);

		// Token: 0x06006366 RID: 25446
		public abstract void StepIn(Response response, Table arguments);

		// Token: 0x06006367 RID: 25447
		public abstract void StepOut(Response response, Table arguments);

		// Token: 0x06006368 RID: 25448
		public abstract void Pause(Response response, Table arguments);

		// Token: 0x06006369 RID: 25449
		public abstract void StackTrace(Response response, Table arguments);

		// Token: 0x0600636A RID: 25450
		public abstract void Scopes(Response response, Table arguments);

		// Token: 0x0600636B RID: 25451
		public abstract void Variables(Response response, Table arguments);

		// Token: 0x0600636C RID: 25452 RVA: 0x0027AE48 File Offset: 0x00279048
		public virtual void Source(Response response, Table arguments)
		{
			this.SendErrorResponse(response, 1020, "Source not supported", null, true, false);
		}

		// Token: 0x0600636D RID: 25453
		public abstract void Threads(Response response, Table arguments);

		// Token: 0x0600636E RID: 25454
		public abstract void Evaluate(Response response, Table arguments);

		// Token: 0x0600636F RID: 25455 RVA: 0x0027AE5E File Offset: 0x0027905E
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

		// Token: 0x06006370 RID: 25456 RVA: 0x0027AE83 File Offset: 0x00279083
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

		// Token: 0x06006371 RID: 25457 RVA: 0x0027AEA8 File Offset: 0x002790A8
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

		// Token: 0x06006372 RID: 25458 RVA: 0x0027AF04 File Offset: 0x00279104
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

		// Token: 0x040055D4 RID: 21972
		private bool _debuggerLinesStartAt1;

		// Token: 0x040055D5 RID: 21973
		private bool _debuggerPathsAreURI;

		// Token: 0x040055D6 RID: 21974
		private bool _clientLinesStartAt1 = true;

		// Token: 0x040055D7 RID: 21975
		private bool _clientPathsAreURI = true;
	}
}
