using System;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x02000D5A RID: 3418
	internal class BuildTimeScopeFrame
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x00272358 File Offset: 0x00270558
		// (set) Token: 0x060060AF RID: 24751 RVA: 0x00272360 File Offset: 0x00270560
		public bool HasVarArgs { get; private set; }

		// Token: 0x060060B0 RID: 24752 RVA: 0x0027236C File Offset: 0x0027056C
		internal BuildTimeScopeFrame(bool hasVarArgs)
		{
			this.HasVarArgs = hasVarArgs;
			this.m_ScopeTreeHead = (this.m_ScopeTreeRoot = new BuildTimeScopeBlock(null));
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x002723A6 File Offset: 0x002705A6
		internal void PushBlock()
		{
			this.m_ScopeTreeHead = this.m_ScopeTreeHead.AddChild();
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x002723B9 File Offset: 0x002705B9
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

		// Token: 0x060060B3 RID: 24755 RVA: 0x002723F5 File Offset: 0x002705F5
		internal RuntimeScopeFrame GetRuntimeFrameData()
		{
			if (this.m_ScopeTreeHead != this.m_ScopeTreeRoot)
			{
				throw new InternalErrorException("Misaligned scope frames/blocks!");
			}
			this.m_ScopeFrame.ToFirstBlock = this.m_ScopeTreeRoot.ScopeBlock.To;
			return this.m_ScopeFrame;
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x00272434 File Offset: 0x00270634
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

		// Token: 0x060060B5 RID: 24757 RVA: 0x00272462 File Offset: 0x00270662
		internal SymbolRef DefineLocal(string name)
		{
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x00272470 File Offset: 0x00270670
		internal SymbolRef TryDefineLocal(string name)
		{
			if (this.m_ScopeTreeHead.Find(name) != null)
			{
				this.m_ScopeTreeHead.Rename(name);
			}
			return this.m_ScopeTreeHead.Define(name);
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x00272498 File Offset: 0x00270698
		internal void ResolveLRefs()
		{
			this.m_ScopeTreeRoot.ResolveGotos();
			this.m_ScopeTreeRoot.ResolveLRefs(this);
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x002724B2 File Offset: 0x002706B2
		internal int AllocVar(SymbolRef var)
		{
			var.i_Index = this.m_ScopeFrame.DebugSymbols.Count;
			this.m_ScopeFrame.DebugSymbols.Add(var);
			return var.i_Index;
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x002724E1 File Offset: 0x002706E1
		internal int GetPosForNextVar()
		{
			return this.m_ScopeFrame.DebugSymbols.Count;
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x002724F3 File Offset: 0x002706F3
		internal void DefineLabel(LabelStatement label)
		{
			this.m_ScopeTreeHead.DefineLabel(label);
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x00272501 File Offset: 0x00270701
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_ScopeTreeHead.RegisterGoto(gotostat);
		}

		// Token: 0x0400553A RID: 21818
		private BuildTimeScopeBlock m_ScopeTreeRoot;

		// Token: 0x0400553B RID: 21819
		private BuildTimeScopeBlock m_ScopeTreeHead;

		// Token: 0x0400553C RID: 21820
		private RuntimeScopeFrame m_ScopeFrame = new RuntimeScopeFrame();
	}
}
