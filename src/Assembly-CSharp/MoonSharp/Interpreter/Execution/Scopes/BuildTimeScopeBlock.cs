using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Tree.Statements;

namespace MoonSharp.Interpreter.Execution.Scopes
{
	// Token: 0x0200116D RID: 4461
	internal class BuildTimeScopeBlock
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06006C91 RID: 27793 RVA: 0x00049FFF File Offset: 0x000481FF
		// (set) Token: 0x06006C92 RID: 27794 RVA: 0x0004A007 File Offset: 0x00048207
		internal BuildTimeScopeBlock Parent { get; private set; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06006C93 RID: 27795 RVA: 0x0004A010 File Offset: 0x00048210
		// (set) Token: 0x06006C94 RID: 27796 RVA: 0x0004A018 File Offset: 0x00048218
		internal List<BuildTimeScopeBlock> ChildNodes { get; private set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06006C95 RID: 27797 RVA: 0x0004A021 File Offset: 0x00048221
		// (set) Token: 0x06006C96 RID: 27798 RVA: 0x0004A029 File Offset: 0x00048229
		internal RuntimeScopeBlock ScopeBlock { get; private set; }

		// Token: 0x06006C97 RID: 27799 RVA: 0x002990D8 File Offset: 0x002972D8
		internal void Rename(string name)
		{
			SymbolRef value = this.m_DefinedNames[name];
			this.m_DefinedNames.Remove(name);
			this.m_DefinedNames.Add(string.Format("@{0}_{1}", name, Guid.NewGuid().ToString("N")), value);
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x0004A032 File Offset: 0x00048232
		internal BuildTimeScopeBlock(BuildTimeScopeBlock parent)
		{
			this.Parent = parent;
			this.ChildNodes = new List<BuildTimeScopeBlock>();
			this.ScopeBlock = new RuntimeScopeBlock();
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x00299128 File Offset: 0x00297328
		internal BuildTimeScopeBlock AddChild()
		{
			BuildTimeScopeBlock buildTimeScopeBlock = new BuildTimeScopeBlock(this);
			this.ChildNodes.Add(buildTimeScopeBlock);
			return buildTimeScopeBlock;
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x0004A062 File Offset: 0x00048262
		internal SymbolRef Find(string name)
		{
			return this.m_DefinedNames.GetOrDefault(name);
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x0029914C File Offset: 0x0029734C
		internal SymbolRef Define(string name)
		{
			SymbolRef symbolRef = SymbolRef.Local(name, -1);
			this.m_DefinedNames.Add(name, symbolRef);
			this.m_LastDefinedName = name;
			return symbolRef;
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x00299178 File Offset: 0x00297378
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

		// Token: 0x06006C9D RID: 27805 RVA: 0x002992DC File Offset: 0x002974DC
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

		// Token: 0x06006C9E RID: 27806 RVA: 0x0004A070 File Offset: 0x00048270
		internal void RegisterGoto(GotoStatement gotostat)
		{
			if (this.m_PendingGotos == null)
			{
				this.m_PendingGotos = new List<GotoStatement>();
			}
			this.m_PendingGotos.Add(gotostat);
			gotostat.SetDefinedVars(this.m_DefinedNames.Count, this.m_LastDefinedName);
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x0029937C File Offset: 0x0029757C
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

		// Token: 0x040061C0 RID: 25024
		private Dictionary<string, SymbolRef> m_DefinedNames = new Dictionary<string, SymbolRef>();

		// Token: 0x040061C1 RID: 25025
		private List<GotoStatement> m_PendingGotos;

		// Token: 0x040061C2 RID: 25026
		private Dictionary<string, LabelStatement> m_LocalLabels;

		// Token: 0x040061C3 RID: 25027
		private string m_LastDefinedName;
	}
}
