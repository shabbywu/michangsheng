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
	// Token: 0x02000D93 RID: 3475
	public class MoonSharpVsCodeDebugServer : IDisposable
	{
		// Token: 0x060062ED RID: 25325 RVA: 0x00279D17 File Offset: 0x00277F17
		public MoonSharpVsCodeDebugServer(int port = 41912)
		{
			this.m_Port = port;
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x00279D48 File Offset: 0x00277F48
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

		// Token: 0x060062EF RID: 25327 RVA: 0x00279DCC File Offset: 0x00277FCC
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

		// Token: 0x060062F0 RID: 25328 RVA: 0x00279E90 File Offset: 0x00278090
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060062F1 RID: 25329 RVA: 0x00279F20 File Offset: 0x00278120
		// (set) Token: 0x060062F2 RID: 25330 RVA: 0x00279F80 File Offset: 0x00278180
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

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060062F3 RID: 25331 RVA: 0x0027A00C File Offset: 0x0027820C
		// (set) Token: 0x060062F4 RID: 25332 RVA: 0x0027A060 File Offset: 0x00278260
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

		// Token: 0x060062F5 RID: 25333 RVA: 0x0027A0E8 File Offset: 0x002782E8
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x0027A1A8 File Offset: 0x002783A8
		// (set) Token: 0x060062F7 RID: 25335 RVA: 0x0027A1B0 File Offset: 0x002783B0
		public Action<string> Logger { get; set; }

		// Token: 0x060062F8 RID: 25336 RVA: 0x0027A1BC File Offset: 0x002783BC
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

		// Token: 0x060062F9 RID: 25337 RVA: 0x0027A200 File Offset: 0x00278400
		public void Dispose()
		{
			this.m_StopEvent.Set();
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x0027A210 File Offset: 0x00278410
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

		// Token: 0x060062FB RID: 25339 RVA: 0x0027A2D4 File Offset: 0x002784D4
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

		// Token: 0x060062FC RID: 25340 RVA: 0x0027A3D0 File Offset: 0x002785D0
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

		// Token: 0x060062FD RID: 25341 RVA: 0x0027A434 File Offset: 0x00278634
		private void Log(string format, params object[] args)
		{
			Action<string> logger = this.Logger;
			if (logger != null)
			{
				string obj = string.Format(format, args);
				logger(obj);
			}
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x0027A45A File Offset: 0x0027865A
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

		// Token: 0x040055A6 RID: 21926
		private object m_Lock = new object();

		// Token: 0x040055A7 RID: 21927
		private List<AsyncDebugger> m_DebuggerList = new List<AsyncDebugger>();

		// Token: 0x040055A8 RID: 21928
		private AsyncDebugger m_Current;

		// Token: 0x040055A9 RID: 21929
		private ManualResetEvent m_StopEvent = new ManualResetEvent(false);

		// Token: 0x040055AA RID: 21930
		private bool m_Started;

		// Token: 0x040055AB RID: 21931
		private int m_Port;
	}
}
