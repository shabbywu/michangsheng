using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x02000D59 RID: 3417
	internal class BuildTimeScopeBlock
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x00271EEA File Offset: 0x002700EA
		// (set) Token: 0x060060A0 RID: 24736 RVA: 0x00271EF2 File Offset: 0x002700F2
		internal BuildTimeScopeBlock Parent { get; private set; }

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060060A1 RID: 24737 RVA: 0x00271EFB File Offset: 0x002700FB
		// (set) Token: 0x060060A2 RID: 24738 RVA: 0x00271F03 File Offset: 0x00270103
		internal List<BuildTimeScopeBlock> ChildNodes { get; private set; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x00271F0C File Offset: 0x0027010C
		// (set) Token: 0x060060A4 RID: 24740 RVA: 0x00271F14 File Offset: 0x00270114
		internal RuntimeScopeBlock ScopeBlock { get; private set; }

		// Token: 0x060060A5 RID: 24741 RVA: 0x00271F20 File Offset: 0x00270120
		internal void Rename(string name)
		{
			SymbolRef value = this.m_DefinedNames[name];
			this.m_DefinedNames.Remove(name);
			this.m_DefinedNames.Add(string.Format("@{0}_{1}", name, Guid.NewGuid().ToString("N")), value);
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x00271F70 File Offset: 0x00270170
		internal BuildTimeScopeBlock(BuildTimeScopeBlock parent)
		{
			this.Parent = parent;
			this.ChildNodes = new List<BuildTimeScopeBlock>();
			this.ScopeBlock = new RuntimeScopeBlock();
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x00271FA0 File Offset: 0x002701A0
		internal BuildTimeScopeBlock AddChild()
		{
			BuildTimeScopeBlock buildTimeScopeBlock = new BuildTimeScopeBlock(this);
			this.ChildNodes.Add(buildTimeScopeBlock);
			return buildTimeScopeBlock;
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x00271FC1 File Offset: 0x002701C1
		internal SymbolRef Find(string name)
		{
			return this.m_DefinedNames.GetOrDefault(name);
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x00271FD0 File Offset: 0x002701D0
		internal SymbolRef Define(string name)
		{
			SymbolRef symbolRef = SymbolRef.Local(name, -1);
			this.m_DefinedNames.Add(name, symbolRef);
			this.m_LastDefinedName = name;
			return symbolRef;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x00271FFC File Offset: 0x002701FC
		internal int ResolveLRefs(BuildTimeScopeFrame buildTimeScopeFrame)
		{
			int num = -1;
			int to = -1;
			foreach (SymbolRef var in this.m_DefinedNames.Values)
			{
				int num2 = buildTimeScopeFrame.AllocVar(var);
				if (num < 0)
				{
					num = num2;
				}
				to = num2;
			}
			this.ScopeBlock.From = num;
			this.ScopeBlock.ToInclusive = (this.ScopeBlock.To = to);
			if (num < 0)
			{
				this.ScopeBlock.From = buildTimeScopeFrame.GetPosForNextVar();
			}
			foreach (BuildTimeScopeBlock buildTimeScopeBlock in this.ChildNodes)
			{
				this.ScopeBlock.ToInclusive = Math.Max(this.ScopeBlock.ToInclusive, buildTimeScopeBlock.ResolveLRefs(buildTimeScopeFrame));
			}
			if (this.m_LocalLabels != null)
			{
				foreach (LabelStatement labelStatement in this.m_LocalLabels.Values)
				{
					labelStatement.SetScope(this.ScopeBlock);
				}
			}
			return this.ScopeBlock.ToInclusive;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x00272160 File Offset: 0x00270360
		internal void DefineLabel(LabelStatement label)
		{
			if (this.m_LocalLabels == null)
			{
				this.m_LocalLabels = new Dictionary<string, LabelStatement>();
			}
			if (this.m_LocalLabels.ContainsKey(label.Label))
			{
				throw new SyntaxErrorException(label.NameToken, "label '{0}' already defined on line {1}", new object[]
				{
					label.Label,
					this.m_LocalLabels[label.Label].SourceRef.FromLine
				});
			}
			this.m_LocalLabels.Add(label.Label, label);
			label.SetDefinedVars(this.m_DefinedNames.Count, this.m_LastDefinedName);
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x002721FF File Offset: 0x002703FF
		internal void RegisterGoto(GotoStatement gotostat)
		{
			if (this.m_PendingGotos == null)
			{
				this.m_PendingGotos = new List<GotoStatement>();
			}
			this.m_PendingGotos.Add(gotostat);
			gotostat.SetDefinedVars(this.m_DefinedNames.Count, this.m_LastDefinedName);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x00272238 File Offset: 0x00270438
		internal void ResolveGotos()
		{
			if (this.m_PendingGotos == null)
			{
				return;
			}
			foreach (GotoStatement gotoStatement in this.m_PendingGotos)
			{
				LabelStatement labelStatement;
				if (this.m_LocalLabels != null && this.m_LocalLabels.TryGetValue(gotoStatement.Label, out labelStatement))
				{
					if (labelStatement.DefinedVarsCount > gotoStatement.DefinedVarsCount)
					{
						throw new SyntaxErrorException(gotoStatement.GotoToken, "<goto {0}> at line {1} jumps into the scope of local '{2}'", new object[]
						{
							gotoStatement.Label,
							gotoStatement.GotoToken.FromLine,
							labelStatement.LastDefinedVarName
						});
					}
					labelStatement.RegisterGoto(gotoStatement);
				}
				else
				{
					if (this.Parent == null)
					{
						throw new SyntaxErrorException(gotoStatement.GotoToken, "no visible label '{0}' for <goto> at line {1}", new object[]
						{
							gotoStatement.Label,
							gotoStatement.GotoToken.FromLine
						});
					}
					this.Parent.RegisterGoto(gotoStatement);
				}
			}
			this.m_PendingGotos.Clear();
		}

		// Token: 0x04005536 RID: 21814
		private Dictionary<string, SymbolRef> m_DefinedNames = new Dictionary<string, SymbolRef>();

		// Token: 0x04005537 RID: 21815
		private List<GotoStatement> m_PendingGotos;

		// Token: 0x04005538 RID: 21816
		private Dictionary<string, LabelStatement> m_LocalLabels;

		// Token: 0x04005539 RID: 21817
		private string m_LastDefinedName;
	}
}
