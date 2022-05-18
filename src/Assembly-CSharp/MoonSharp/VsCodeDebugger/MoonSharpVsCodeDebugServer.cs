using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.VsCodeDebugger.DebuggerLogic;
using MoonSharp.VsCodeDebugger.SDK;

namespace MoonSharp.VsCodeDebugger
{
	// Token: 0x020011B6 RID: 4534
	public class MoonSharpVsCodeDebugServer : IDisposable
	{
		// Token: 0x06006F1E RID: 28446 RVA: 0x0004B851 File Offset: 0x00049A51
		public MoonSharpVsCodeDebugServer(int port = 41912)
		{
			this.m_Port = port;
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x0029F9C0 File Offset: 0x0029DBC0
		[Obsolete("Use the constructor taking only a port, and the 'Attach' method instead.")]
		public MoonSharpVsCodeDebugServer(Script script, int port, Func<SourceCode, string> sourceFinder = null)
		{
			this.m_Port = port;
			Func<SourceCode, string> sourceFinder2 = sourceFinder;
			if (sourceFinder == null && (sourceFinder2 = MoonSharpVsCodeDebugServer.<>c.<>9__7_0) == null)
			{
				sourceFinder2 = (MoonSharpVsCodeDebugServer.<>c.<>9__7_0 = ((SourceCode s) => s.Name));
			}
			this.m_Current = new AsyncDebugger(script, sourceFinder2, "Default script");
			this.m_DebuggerList.Add(this.m_Current);
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x0029FA44 File Offset: 0x0029DC44
		public void AttachToScript(Script script, string name, Func<SourceCode, string> sourceFinder = null)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.m_DebuggerList.Any((AsyncDebugger d) => d.Script == script))
				{
					throw new ArgumentException("Script already attached to this debugger.");
				}
				Script script2 = script;
				Func<SourceCode, string> sourceFinder2 = sourceFinder;
				if (sourceFinder == null && (sourceFinder2 = MoonSharpVsCodeDebugServer.<>c.<>9__8_1) == null)
				{
					sourceFinder2 = (MoonSharpVsCodeDebugServer.<>c.<>9__8_1 = ((SourceCode s) => s.Name));
				}
				AsyncDebugger asyncDebugger = new AsyncDebugger(script2, sourceFinder2, name);
				script.AttachDebugger(asyncDebugger);
				this.m_DebuggerList.Add(asyncDebugger);
				if (this.m_Current == null)
				{
					this.m_Current = asyncDebugger;
				}
			}
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x0029FB08 File Offset: 0x0029DD08
		public IEnumerable<KeyValuePair<int, string>> GetAttachedDebuggersByIdAndName()
		{
			object @lock = this.m_Lock;
			IEnumerable<KeyValuePair<int, string>> result;
			lock (@lock)
			{
				result = (from d in this.m_DebuggerList
				orderby d.Id
				select new KeyValuePair<int, string>(d.Id, d.Name)).ToArray<KeyValuePair<int, string>>();
			}
			return result;
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06006F22 RID: 28450 RVA: 0x0029FB98 File Offset: 0x0029DD98
		// (set) Token: 0x06006F23 RID: 28451 RVA: 0x0029FBF8 File Offset: 0x0029DDF8
		public int? CurrentId
		{
			get
			{
				object @lock = this.m_Lock;
				int? num;
				lock (@lock)
				{
					int? num2;
					if (this.m_Current == null)
					{
						num = null;
						num2 = num;
					}
					else
					{
						num2 = new int?(this.m_Current.Id);
					}
					num = num2;
				}
				return num;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (value == null)
					{
						this.m_Current = null;
					}
					else
					{
						AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault(delegate(AsyncDebugger d)
						{
							int id = d.Id;
							int? value2 = value;
							return id == value2.GetValueOrDefault() & value2 != null;
						});
						if (asyncDebugger == null)
						{
							throw new ArgumentException("Cannot find debugger with given Id.");
						}
						this.m_Current = asyncDebugger;
					}
				}
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06006F24 RID: 28452 RVA: 0x0029FC84 File Offset: 0x0029DE84
		// (set) Token: 0x06006F25 RID: 28453 RVA: 0x0029FCD8 File Offset: 0x0029DED8
		public Script Current
		{
			get
			{
				object @lock = this.m_Lock;
				Script result;
				lock (@lock)
				{
					result = ((this.m_Current != null) ? this.m_Current.Script : null);
				}
				return result;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (value == null)
					{
						this.m_Current = null;
					}
					else
					{
						AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == value);
						if (asyncDebugger == null)
						{
							throw new ArgumentException("Cannot find debugger with given script associated.");
						}
						this.m_Current = asyncDebugger;
					}
				}
			}
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x0029FD60 File Offset: 0x0029DF60
		public void Detach(Script script)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				AsyncDebugger asyncDebugger = this.m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == script);
				if (asyncDebugger == null)
				{
					throw new ArgumentException("Cannot detach script - not found.");
				}
				asyncDebugger.Client = null;
				this.m_DebuggerList.Remove(asyncDebugger);
				if (this.m_Current == asyncDebugger)
				{
					if (this.m_DebuggerList.Count > 0)
					{
						this.m_Current = this.m_DebuggerList[this.m_DebuggerList.Count - 1];
					}
					else
					{
						this.m_Current = null;
					}
				}
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06006F27 RID: 28455 RVA: 0x0004B882 File Offset: 0x00049A82
		// (set) Token: 0x06006F28 RID: 28456 RVA: 0x0004B88A File Offset: 0x00049A8A
		public Action<string> Logger { get; set; }

		// Token: 0x06006F29 RID: 28457 RVA: 0x0029FE20 File Offset: 0x0029E020
		[Obsolete("Use the Attach method instead.")]
		public IDebugger GetDebugger()
		{
			object @lock = this.m_Lock;
			IDebugger current;
			lock (@lock)
			{
				current = this.m_Current;
			}
			return current;
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x0004B893 File Offset: 0x00049A93
		public void Dispose()
		{
			this.m_StopEvent.Set();
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x0029FE64 File Offset: 0x0029E064
		public MoonSharpVsCodeDebugServer Start()
		{
			object @lock = this.m_Lock;
			bool flag = false;
			try
			{
				Monitor.Enter(@lock, ref flag);
				if (this.m_Started)
				{
					throw new InvalidOperationException("Cannot start; server has already been started.");
				}
				this.m_StopEvent.Reset();
				TcpListener serverSocket = null;
				serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), this.m_Port);
				serverSocket.Start();
				MoonSharpVsCodeDebugServer.SpawnThread("VsCodeDebugServer_" + this.m_Port.ToString(), delegate
				{
					this.ListenThread(serverSocket);
				});
				this.m_Started = true;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(@lock);
				}
			}
			return this;
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x0029FF28 File Offset: 0x0029E128
		private void ListenThread(TcpListener serverSocket)
		{
			try
			{
				while (!this.m_StopEvent.WaitOne(0))
				{
					Socket clientSocket = serverSocket.AcceptSocket();
					if (clientSocket != null)
					{
						string sessionId = Guid.NewGuid().ToString("N");
						this.Log("[{0}] : Accepted connection from client {1}", new object[]
						{
							sessionId,
							clientSocket.RemoteEndPoint
						});
						MoonSharpVsCodeDebugServer.SpawnThread("VsCodeDebugSession_" + sessionId, delegate
						{
							using (NetworkStream networkStream = new NetworkStream(clientSocket))
							{
								try
								{
									this.RunSession(sessionId, networkStream);
								}
								catch (Exception ex2)
								{
									this.Log("[{0}] : Error : {1}", new object[]
									{
										ex2.Message
									});
								}
							}
							clientSocket.Close();
							this.Log("[{0}] : Client connection closed", new object[]
							{
								sessionId
							});
						});
					}
				}
			}
			catch (Exception ex)
			{
				this.Log("Fatal error in listening thread : {0}", new object[]
				{
					ex.Message
				});
			}
			finally
			{
				if (serverSocket != null)
				{
					serverSocket.Stop();
				}
			}
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x002A0024 File Offset: 0x0029E224
		private void RunSession(string sessionId, NetworkStream stream)
		{
			DebugSession debugSession = null;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.m_Current != null)
				{
					debugSession = new MoonSharpDebugSession(this, this.m_Current);
				}
				else
				{
					debugSession = new EmptyDebugSession(this);
				}
			}
			debugSession.ProcessLoop(stream, stream);
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x002A0088 File Offset: 0x0029E288
		private void Log(string format, params object[] args)
		{
			Action<string> logger = this.Logger;
			if (logger != null)
			{
				string obj = string.Format(format, args);
				logger(obj);
			}
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0004B8A1 File Offset: 0x00049AA1
		private static void SpawnThread(string name, Action threadProc)
		{
			new System.Threading.Thread(delegate()
			{
				threadProc();
			})
			{
				IsBackground = true,
				Name = name
			}.Start();
		}

		// Token: 0x0400627D RID: 25213
		private object m_Lock = new object();

		// Token: 0x0400627E RID: 25214
		private List<AsyncDebugger> m_DebuggerList = new List<AsyncDebugger>();

		// Token: 0x0400627F RID: 25215
		private AsyncDebugger m_Current;

		// Token: 0x04006280 RID: 25216
		private ManualResetEvent m_StopEvent = new ManualResetEvent(false);

		// Token: 0x04006281 RID: 25217
		private bool m_Started;

		// Token: 0x04006282 RID: 25218
		private int m_Port;
	}
}
