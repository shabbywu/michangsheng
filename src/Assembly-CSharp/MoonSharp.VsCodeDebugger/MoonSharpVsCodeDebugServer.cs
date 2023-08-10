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

namespace MoonSharp.VsCodeDebugger;

public class MoonSharpVsCodeDebugServer : IDisposable
{
	private object m_Lock = new object();

	private List<AsyncDebugger> m_DebuggerList = new List<AsyncDebugger>();

	private AsyncDebugger m_Current;

	private ManualResetEvent m_StopEvent = new ManualResetEvent(initialState: false);

	private bool m_Started;

	private int m_Port;

	public int? CurrentId
	{
		get
		{
			lock (m_Lock)
			{
				return (m_Current != null) ? new int?(m_Current.Id) : null;
			}
		}
		set
		{
			lock (m_Lock)
			{
				if (!value.HasValue)
				{
					m_Current = null;
					return;
				}
				AsyncDebugger asyncDebugger = m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Id == value);
				if (asyncDebugger == null)
				{
					throw new ArgumentException("Cannot find debugger with given Id.");
				}
				m_Current = asyncDebugger;
			}
		}
	}

	public Script Current
	{
		get
		{
			lock (m_Lock)
			{
				return (m_Current != null) ? m_Current.Script : null;
			}
		}
		set
		{
			lock (m_Lock)
			{
				if (value == null)
				{
					m_Current = null;
					return;
				}
				AsyncDebugger asyncDebugger = m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == value);
				if (asyncDebugger == null)
				{
					throw new ArgumentException("Cannot find debugger with given script associated.");
				}
				m_Current = asyncDebugger;
			}
		}
	}

	public Action<string> Logger { get; set; }

	public MoonSharpVsCodeDebugServer(int port = 41912)
	{
		m_Port = port;
	}

	[Obsolete("Use the constructor taking only a port, and the 'Attach' method instead.")]
	public MoonSharpVsCodeDebugServer(Script script, int port, Func<SourceCode, string> sourceFinder = null)
	{
		m_Port = port;
		m_Current = new AsyncDebugger(script, sourceFinder ?? ((Func<SourceCode, string>)((SourceCode s) => s.Name)), "Default script");
		m_DebuggerList.Add(m_Current);
	}

	public void AttachToScript(Script script, string name, Func<SourceCode, string> sourceFinder = null)
	{
		lock (m_Lock)
		{
			if (m_DebuggerList.Any((AsyncDebugger d) => d.Script == script))
			{
				throw new ArgumentException("Script already attached to this debugger.");
			}
			AsyncDebugger asyncDebugger = new AsyncDebugger(script, sourceFinder ?? ((Func<SourceCode, string>)((SourceCode s) => s.Name)), name);
			script.AttachDebugger(asyncDebugger);
			m_DebuggerList.Add(asyncDebugger);
			if (m_Current == null)
			{
				m_Current = asyncDebugger;
			}
		}
	}

	public IEnumerable<KeyValuePair<int, string>> GetAttachedDebuggersByIdAndName()
	{
		lock (m_Lock)
		{
			return (from d in m_DebuggerList
				orderby d.Id
				select new KeyValuePair<int, string>(d.Id, d.Name)).ToArray();
		}
	}

	public void Detach(Script script)
	{
		lock (m_Lock)
		{
			AsyncDebugger asyncDebugger = m_DebuggerList.FirstOrDefault((AsyncDebugger d) => d.Script == script);
			if (asyncDebugger == null)
			{
				throw new ArgumentException("Cannot detach script - not found.");
			}
			asyncDebugger.Client = null;
			m_DebuggerList.Remove(asyncDebugger);
			if (m_Current == asyncDebugger)
			{
				if (m_DebuggerList.Count > 0)
				{
					m_Current = m_DebuggerList[m_DebuggerList.Count - 1];
				}
				else
				{
					m_Current = null;
				}
			}
		}
	}

	[Obsolete("Use the Attach method instead.")]
	public IDebugger GetDebugger()
	{
		lock (m_Lock)
		{
			return m_Current;
		}
	}

	public void Dispose()
	{
		m_StopEvent.Set();
	}

	public MoonSharpVsCodeDebugServer Start()
	{
		lock (m_Lock)
		{
			if (m_Started)
			{
				throw new InvalidOperationException("Cannot start; server has already been started.");
			}
			m_StopEvent.Reset();
			TcpListener serverSocket = null;
			serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), m_Port);
			serverSocket.Start();
			SpawnThread("VsCodeDebugServer_" + m_Port, delegate
			{
				ListenThread(serverSocket);
			});
			m_Started = true;
			return this;
		}
	}

	private void ListenThread(TcpListener serverSocket)
	{
		try
		{
			while (!m_StopEvent.WaitOne(0))
			{
				Socket clientSocket = serverSocket.AcceptSocket();
				if (clientSocket == null)
				{
					continue;
				}
				string sessionId = Guid.NewGuid().ToString("N");
				Log("[{0}] : Accepted connection from client {1}", sessionId, clientSocket.RemoteEndPoint);
				SpawnThread("VsCodeDebugSession_" + sessionId, delegate
				{
					using (NetworkStream stream = new NetworkStream(clientSocket))
					{
						try
						{
							RunSession(sessionId, stream);
						}
						catch (Exception ex2)
						{
							Log("[{0}] : Error : {1}", ex2.Message);
						}
					}
					clientSocket.Close();
					Log("[{0}] : Client connection closed", sessionId);
				});
			}
		}
		catch (Exception ex)
		{
			Log("Fatal error in listening thread : {0}", ex.Message);
		}
		finally
		{
			serverSocket?.Stop();
		}
	}

	private void RunSession(string sessionId, NetworkStream stream)
	{
		DebugSession debugSession = null;
		lock (m_Lock)
		{
			debugSession = ((m_Current == null) ? ((DebugSession)new EmptyDebugSession(this)) : ((DebugSession)new MoonSharpDebugSession(this, m_Current)));
		}
		debugSession.ProcessLoop(stream, stream);
	}

	private void Log(string format, params object[] args)
	{
		Action<string> logger = Logger;
		if (logger != null)
		{
			string obj = string.Format(format, args);
			logger(obj);
		}
	}

	private static void SpawnThread(string name, Action threadProc)
	{
		System.Threading.Thread thread = new System.Threading.Thread((ThreadStart)delegate
		{
			threadProc();
		});
		thread.IsBackground = true;
		thread.Name = name;
		thread.Start();
	}
}
