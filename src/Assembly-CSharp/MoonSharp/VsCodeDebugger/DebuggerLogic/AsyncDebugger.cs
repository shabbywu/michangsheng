using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.VsCodeDebugger.DebuggerLogic
{
	// Token: 0x02000DB2 RID: 3506
	internal class AsyncDebugger : IDebugger
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x0027B584 File Offset: 0x00279784
		// (set) Token: 0x0600639F RID: 25503 RVA: 0x0027B58C File Offset: 0x0027978C
		public DebugService DebugService { get; private set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x0027B595 File Offset: 0x00279795
		// (set) Token: 0x060063A1 RID: 25505 RVA: 0x0027B59D File Offset: 0x0027979D
		public Regex ErrorRegex { get; set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x0027B5A6 File Offset: 0x002797A6
		// (set) Token: 0x060063A3 RID: 25507 RVA: 0x0027B5AE File Offset: 0x002797AE
		public Script Script { get; private set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x0027B5B7 File Offset: 0x002797B7
		// (set) Token: 0x060063A5 RID: 25509 RVA: 0x0027B5BF File Offset: 0x002797BF
		public bool PauseRequested { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060063A6 RID: 25510 RVA: 0x0027B5C8 File Offset: 0x002797C8
		// (set) Token: 0x060063A7 RID: 25511 RVA: 0x0027B5D0 File Offset: 0x002797D0
		public string Name { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x0027B5D9 File Offset: 0x002797D9
		// (set) Token: 0x060063A9 RID: 25513 RVA: 0x0027B5E1 File Offset: 0x002797E1
		public int Id { get; private set; }

		// Token: 0x060063AA RID: 25514 RVA: 0x0027B5EC File Offset: 0x002797EC
		public AsyncDebugger(Script script, Func<SourceCode, string> sourceFinder, string name)
		{
			object obj = AsyncDebugger.s_AsyncDebuggerIdLock;
			lock (obj)
			{
				this.Id = AsyncDebugger.s_AsyncDebuggerIdCounter++;
			}
			this.m_SourceFinder = sourceFinder;
			this.ErrorRegex = new Regex("\\A.*\\Z");
			this.Script = script;
			this.m_WatchItems = new List<WatchItem>[6];
			this.Name = name;
			for (int i = 0; i < this.m_WatchItems.Length; i++)
			{
				this.m_WatchItems[i] = new List<WatchItem>(64);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060063AB RID: 25515 RVA: 0x0027B6B4 File Offset: 0x002798B4
		// (set) Token: 0x060063AC RID: 25516 RVA: 0x0027B6BC File Offset: 0x002798BC
		public IAsyncDebuggerClient Client
		{
			get
			{
				return this.m_Client__;
			}
			set
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (this.m_Client__ != null && this.m_Client__ != value)
					{
						this.m_Client__.Unbind();
					}
					if (value != null)
					{
						for (int i = 0; i < this.Script.SourceCodeCount; i++)
						{
							if (this.m_SourcesMap.ContainsKey(i))
							{
								value.OnSourceCodeChanged(i);
							}
						}
					}
					this.m_Client__ = value;
				}
			}
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x0027B748 File Offset: 0x00279948
		DebuggerAction IDebugger.GetAction(int ip, SourceRef sourceref)
		{
			this.PauseRequested = false;
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.SendStopEvent();
				}
			}
			DebuggerAction result;
			for (;;)
			{
				@lock = this.m_Lock;
				lock (@lock)
				{
					if (this.Client == null)
					{
						result = new DebuggerAction
						{
							Action = DebuggerAction.ActionType.Run
						};
						break;
					}
					if (this.m_PendingAction != null)
					{
						DebuggerAction pendingAction = this.m_PendingAction;
						this.m_PendingAction = null;
						result = pendingAction;
						break;
					}
				}
				this.Sleep(10);
			}
			return result;
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x0027B800 File Offset: 0x00279A00
		public void QueueAction(DebuggerAction action)
		{
			for (;;)
			{
				object @lock = this.m_Lock;
				lock (@lock)
				{
					if (this.m_PendingAction == null)
					{
						this.m_PendingAction = action;
						break;
					}
				}
				this.Sleep(10);
			}
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x0027B858 File Offset: 0x00279A58
		private void Sleep(int v)
		{
			Thread.Sleep(10);
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x0027B864 File Offset: 0x00279A64
		private DynamicExpression CreateDynExpr(string code)
		{
			DynamicExpression result;
			try
			{
				result = this.Script.CreateDynamicExpression(code);
			}
			catch (Exception ex)
			{
				result = this.Script.CreateConstantDynamicExpression(code, DynValue.NewString(ex.Message));
			}
			return result;
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x0027B8AC File Offset: 0x00279AAC
		List<DynamicExpression> IDebugger.GetWatchItems()
		{
			return new List<DynamicExpression>();
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x0027B8B3 File Offset: 0x00279AB3
		bool IDebugger.IsPauseRequested()
		{
			return this.PauseRequested;
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00004095 File Offset: 0x00002295
		void IDebugger.RefreshBreakpoints(IEnumerable<SourceRef> refs)
		{
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00004095 File Offset: 0x00002295
		void IDebugger.SetByteCode(string[] byteCode)
		{
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x0027B8BC File Offset: 0x00279ABC
		void IDebugger.SetSourceCode(SourceCode sourceCode)
		{
			this.m_SourcesMap[sourceCode.SourceID] = sourceCode;
			bool flag = false;
			string text = this.m_SourceFinder(sourceCode);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					if (!File.Exists(text))
					{
						flag = true;
					}
					goto IL_3C;
				}
				catch
				{
					flag = true;
					goto IL_3C;
				}
			}
			flag = true;
			IL_3C:
			if (flag)
			{
				text = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".lua");
				File.WriteAllText(text, sourceCode.Code + this.GetFooterForTempFile());
				this.m_SourcesOverride[sourceCode.SourceID] = text;
			}
			else if (text != sourceCode.Name)
			{
				this.m_SourcesOverride[sourceCode.SourceID] = text;
			}
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnSourceCodeChanged(sourceCode.SourceID);
				}
			}
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x0027B9D0 File Offset: 0x00279BD0
		private string GetFooterForTempFile()
		{
			return "\n\n----------------------------------------------------------------------------------------------------------\n-- This file has been generated by the debugger as a placeholder for a script snippet stored in memory. --\n-- If you restart the host process, the contents of this file are not valid anymore.                    --\n----------------------------------------------------------------------------------------------------------\n";
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x0027B9D7 File Offset: 0x00279BD7
		public string GetSourceFile(int sourceId)
		{
			if (this.m_SourcesOverride.ContainsKey(sourceId))
			{
				return this.m_SourcesOverride[sourceId];
			}
			if (this.m_SourcesMap.ContainsKey(sourceId))
			{
				return this.m_SourcesMap[sourceId].Name;
			}
			return null;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x0027BA15 File Offset: 0x00279C15
		public bool IsSourceOverride(int sourceId)
		{
			return this.m_SourcesOverride.ContainsKey(sourceId);
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x0027BA24 File Offset: 0x00279C24
		void IDebugger.SignalExecutionEnded()
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnExecutionEnded();
				}
			}
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x0027BA74 File Offset: 0x00279C74
		bool IDebugger.SignalRuntimeException(ScriptRuntimeException ex)
		{
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client == null)
				{
					return false;
				}
			}
			this.Client.OnException(ex);
			this.PauseRequested = this.ErrorRegex.IsMatch(ex.Message);
			return this.PauseRequested;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x0027BAE8 File Offset: 0x00279CE8
		void IDebugger.Update(WatchType watchType, IEnumerable<WatchItem> items)
		{
			List<WatchItem> list = this.m_WatchItems[(int)watchType];
			list.Clear();
			list.AddRange(items);
			object @lock = this.m_Lock;
			lock (@lock)
			{
				if (this.Client != null)
				{
					this.Client.OnWatchesUpdated(watchType);
				}
			}
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x0027BB4C File Offset: 0x00279D4C
		public List<WatchItem> GetWatches(WatchType watchType)
		{
			return this.m_WatchItems[(int)watchType];
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x0027BB56 File Offset: 0x00279D56
		public SourceCode GetSource(int id)
		{
			if (this.m_SourcesMap.ContainsKey(id))
			{
				return this.m_SourcesMap[id];
			}
			return null;
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0027BB74 File Offset: 0x00279D74
		public SourceCode FindSourceByName(string path)
		{
			path = path.Replace('\\', '/').ToUpperInvariant();
			foreach (KeyValuePair<int, string> keyValuePair in this.m_SourcesOverride)
			{
				if (keyValuePair.Value.Replace('\\', '/').ToUpperInvariant() == path)
				{
					return this.m_SourcesMap[keyValuePair.Key];
				}
			}
			return this.m_SourcesMap.Values.FirstOrDefault((SourceCode s) => s.Name.Replace('\\', '/').ToUpperInvariant() == path);
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0027BC3C File Offset: 0x00279E3C
		void IDebugger.SetDebugService(DebugService debugService)
		{
			this.DebugService = debugService;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0027BC45 File Offset: 0x00279E45
		public DynValue Evaluate(string expression)
		{
			return this.CreateDynExpr(expression).Evaluate(null);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0016F21F File Offset: 0x0016D41F
		DebuggerCaps IDebugger.GetDebuggerCaps()
		{
			return DebuggerCaps.CanDebugSourceCode | DebuggerCaps.HasLineBasedBreakpoints;
		}

		// Token: 0x040055F0 RID: 22000
		private static object s_AsyncDebuggerIdLock = new object();

		// Token: 0x040055F1 RID: 22001
		private static int s_AsyncDebuggerIdCounter = 0;

		// Token: 0x040055F2 RID: 22002
		private object m_Lock = new object();

		// Token: 0x040055F3 RID: 22003
		private IAsyncDebuggerClient m_Client__;

		// Token: 0x040055F4 RID: 22004
		private DebuggerAction m_PendingAction;

		// Token: 0x040055F5 RID: 22005
		private List<WatchItem>[] m_WatchItems;

		// Token: 0x040055F6 RID: 22006
		private Dictionary<int, SourceCode> m_SourcesMap = new Dictionary<int, SourceCode>();

		// Token: 0x040055F7 RID: 22007
		private Dictionary<int, string> m_SourcesOverride = new Dictionary<int, string>();

		// Token: 0x040055F8 RID: 22008
		private Func<SourceCode, string> m_SourceFinder;
	}
}
