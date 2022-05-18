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
	// Token: 0x020011DF RID: 4575
	internal class AsyncDebugger : IDebugger
	{
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06006FE6 RID: 28646 RVA: 0x0004C0BB File Offset: 0x0004A2BB
		// (set) Token: 0x06006FE7 RID: 28647 RVA: 0x0004C0C3 File Offset: 0x0004A2C3
		public DebugService DebugService { get; private set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		// (set) Token: 0x06006FE9 RID: 28649 RVA: 0x0004C0D4 File Offset: 0x0004A2D4
		public Regex ErrorRegex { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x0004C0DD File Offset: 0x0004A2DD
		// (set) Token: 0x06006FEB RID: 28651 RVA: 0x0004C0E5 File Offset: 0x0004A2E5
		public Script Script { get; private set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x0004C0EE File Offset: 0x0004A2EE
		// (set) Token: 0x06006FED RID: 28653 RVA: 0x0004C0F6 File Offset: 0x0004A2F6
		public bool PauseRequested { get; set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06006FEE RID: 28654 RVA: 0x0004C0FF File Offset: 0x0004A2FF
		// (set) Token: 0x06006FEF RID: 28655 RVA: 0x0004C107 File Offset: 0x0004A307
		public string Name { get; set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x0004C110 File Offset: 0x0004A310
		// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x0004C118 File Offset: 0x0004A318
		public int Id { get; private set; }

		// Token: 0x06006FF2 RID: 28658 RVA: 0x002A0BA8 File Offset: 0x0029EDA8
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06006FF3 RID: 28659 RVA: 0x0004C121 File Offset: 0x0004A321
		// (set) Token: 0x06006FF4 RID: 28660 RVA: 0x002A0C70 File Offset: 0x0029EE70
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

		// Token: 0x06006FF5 RID: 28661 RVA: 0x002A0CFC File Offset: 0x0029EEFC
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

		// Token: 0x06006FF6 RID: 28662 RVA: 0x002A0DB4 File Offset: 0x0029EFB4
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

		// Token: 0x06006FF7 RID: 28663 RVA: 0x0004C129 File Offset: 0x0004A329
		private void Sleep(int v)
		{
			Thread.Sleep(10);
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x002A0E0C File Offset: 0x0029F00C
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

		// Token: 0x06006FF9 RID: 28665 RVA: 0x0004C132 File Offset: 0x0004A332
		List<DynamicExpression> IDebugger.GetWatchItems()
		{
			return new List<DynamicExpression>();
		}

		// Token: 0x06006FFA RID: 28666 RVA: 0x0004C139 File Offset: 0x0004A339
		bool IDebugger.IsPauseRequested()
		{
			return this.PauseRequested;
		}

		// Token: 0x06006FFB RID: 28667 RVA: 0x000042DD File Offset: 0x000024DD
		void IDebugger.RefreshBreakpoints(IEnumerable<SourceRef> refs)
		{
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x000042DD File Offset: 0x000024DD
		void IDebugger.SetByteCode(string[] byteCode)
		{
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x002A0E54 File Offset: 0x0029F054
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

		// Token: 0x06006FFE RID: 28670 RVA: 0x0004C141 File Offset: 0x0004A341
		private string GetFooterForTempFile()
		{
			return "\n\n----------------------------------------------------------------------------------------------------------\n-- This file has been generated by the debugger as a placeholder for a script snippet stored in memory. --\n-- If you restart the host process, the contents of this file are not valid anymore.                    --\n----------------------------------------------------------------------------------------------------------\n";
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x0004C148 File Offset: 0x0004A348
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

		// Token: 0x06007000 RID: 28672 RVA: 0x0004C186 File Offset: 0x0004A386
		public bool IsSourceOverride(int sourceId)
		{
			return this.m_SourcesOverride.ContainsKey(sourceId);
		}

		// Token: 0x06007001 RID: 28673 RVA: 0x002A0F68 File Offset: 0x0029F168
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

		// Token: 0x06007002 RID: 28674 RVA: 0x002A0FB8 File Offset: 0x0029F1B8
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

		// Token: 0x06007003 RID: 28675 RVA: 0x002A102C File Offset: 0x0029F22C
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

		// Token: 0x06007004 RID: 28676 RVA: 0x0004C194 File Offset: 0x0004A394
		public List<WatchItem> GetWatches(WatchType watchType)
		{
			return this.m_WatchItems[(int)watchType];
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x0004C19E File Offset: 0x0004A39E
		public SourceCode GetSource(int id)
		{
			if (this.m_SourcesMap.ContainsKey(id))
			{
				return this.m_SourcesMap[id];
			}
			return null;
		}

		// Token: 0x06007006 RID: 28678 RVA: 0x002A1090 File Offset: 0x0029F290
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

		// Token: 0x06007007 RID: 28679 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		void IDebugger.SetDebugService(DebugService debugService)
		{
			this.DebugService = debugService;
		}

		// Token: 0x06007008 RID: 28680 RVA: 0x0004C1C5 File Offset: 0x0004A3C5
		public DynValue Evaluate(string expression)
		{
			return this.CreateDynExpr(expression).Evaluate(null);
		}

		// Token: 0x06007009 RID: 28681 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		DebuggerCaps IDebugger.GetDebuggerCaps()
		{
			return DebuggerCaps.CanDebugSourceCode | DebuggerCaps.HasLineBasedBreakpoints;
		}

		// Token: 0x040062DA RID: 25306
		private static object s_AsyncDebuggerIdLock = new object();

		// Token: 0x040062DB RID: 25307
		private static int s_AsyncDebuggerIdCounter = 0;

		// Token: 0x040062DC RID: 25308
		private object m_Lock = new object();

		// Token: 0x040062DD RID: 25309
		private IAsyncDebuggerClient m_Client__;

		// Token: 0x040062DE RID: 25310
		private DebuggerAction m_PendingAction;

		// Token: 0x040062DF RID: 25311
		private List<WatchItem>[] m_WatchItems;

		// Token: 0x040062E0 RID: 25312
		private Dictionary<int, SourceCode> m_SourcesMap = new Dictionary<int, SourceCode>();

		// Token: 0x040062E1 RID: 25313
		private Dictionary<int, string> m_SourcesOverride = new Dictionary<int, string>();

		// Token: 0x040062E2 RID: 25314
		private Func<SourceCode, string> m_SourceFinder;
	}
}
