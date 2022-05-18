using System;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x0200116E RID: 4462
	internal class BuildTimeScopeFrame
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06006CA0 RID: 27808 RVA: 0x0004A0A8 File Offset: 0x000482A8
		// (set) Token: 0x06006CA1 RID: 27809 RVA: 0x0004A0B0 File Offset: 0x000482B0
		public bool HasVarArgs { get; private set; }

		// Token: 0x06006CA2 RID: 27810 RVA: 0x0029949C File Offset: 0x0029769C
		internal BuildTimeScopeFrame(bool hasVarArgs)
		{
			this.HasVarArgs = hasVarArgs;
			this.m_ScopeTreeHead = (this.m_ScopeTreeRoot = new BuildTimeScopeBlock(null));
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x0004A0B9 File Offset: 0x000482B9
		internal void PushBlock()
		{
			this.m_ScopeTreeHead = this.m_ScopeTreeHead.AddChild();
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x0004A0CC File Offset: 0x000482CC
		internal RuntimeScopeBlock PopBlock()
		{
			BuildTimeScopeBlock scopeTreeHead = this.m_ScopeTreeHead;
			this.m_ScopeTreeHead.ResolveGotos();
			this.m_ScopeTreeHead = this.m_ScopeTreeHead.Parent;
			if (this.m_ScopeTreeHead == null)
			{
				throw new InternalErrorException("Can't pop block - stack underflow");
			}
			return scopeTreeHead.ScopeBlock;
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x0004A108 File Offset: 0x00048308
		internal RuntimeScopeFrame GetRuntimeFrameData()
		{
			if (this.m_ScopeTreeHead != this.m_ScopeTreeRoot)
			{
				throw new InternalErrorException("Misaligned scope frames/blocks!");
			}
			this.m_ScopeFrame.ToFirstBlock = this.m_ScopeTreeRoot.ScopeBlock.To;
			return this.m_ScopeFrame;
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x002994D8 File Offset: 0x002976D8
		internal SymbolRef Find(string name)
		{
			for (BuildTimeScopeBlock buildTimeScopeBlock = this.m_ScopeTreeHead; buildTimeScopeBlock != null; buildTimeScopeBlock = buildTimeScopeBlock.Parent)
			{
				SymbolRef symbolRef = buildTimeScopeBlock.Find(name);
				if (symbolRef != null)
				{
					return symbolRef;
				}
			}
			return null;
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x0004A144 File Offset: 0x00048344
		internal SymbolRef DefineLocal(string name)
		{
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x0004A152 File Offset: 0x00048352
		internal SymbolRef TryDefineLocal(string name)
		{
			if (this.m_ScopeTreeHead.Find(name) != null)
			{
				this.m_ScopeTreeHead.Rename(name);
			}
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x0004A17A File Offset: 0x0004837A
		internal void ResolveLRefs()
		{
			this.m_ScopeTreeRoot.ResolveGotos();
			this.m_ScopeTreeRoot.ResolveLRefs(this);
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x0004A194 File Offset: 0x00048394
		internal int AllocVar(SymbolRef var)
		{
			var.i_Index = this.m_ScopeFrame.DebugSymbols.Count;
			this.m_ScopeFrame.DebugSymbols.Add(var);
			return var.i_Index;
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x0004A1C3 File Offset: 0x000483C3
		internal int GetPosForNextVar()
		{
			return this.m_ScopeFrame.DebugSymbols.Count;
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x0004A1D5 File Offset: 0x000483D5
		internal void DefineLabel(LabelStatement label)
		{
			this.m_ScopeTreeHead.DefineLabel(label);
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x0004A1E3 File Offset: 0x000483E3
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_ScopeTreeHead.RegisterGoto(gotostat);
		}

		// Token: 0x040061C4 RID: 25028
		private BuildTimeScopeBlock m_ScopeTreeRoot;

		// Token: 0x040061C5 RID: 25029
		private BuildTimeScopeBlock m_ScopeTreeHead;

		// Token: 0x040061C6 RID: 25030
		private RuntimeScopeFrame m_ScopeFrame = new RuntimeScopeFrame();
	}
}
