using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDB RID: 3291
	internal class LabelStatement : Statement
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06005C39 RID: 23609 RVA: 0x0025E8D4 File Offset: 0x0025CAD4
		// (set) Token: 0x06005C3A RID: 23610 RVA: 0x0025E8DC File Offset: 0x0025CADC
		public string Label { get; private set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x0025E8E5 File Offset: 0x0025CAE5
		// (set) Token: 0x06005C3C RID: 23612 RVA: 0x0025E8ED File Offset: 0x0025CAED
		public int Address { get; private set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x0025E8F6 File Offset: 0x0025CAF6
		// (set) Token: 0x06005C3E RID: 23614 RVA: 0x0025E8FE File Offset: 0x0025CAFE
		public SourceRef SourceRef { get; private set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06005C3F RID: 23615 RVA: 0x0025E907 File Offset: 0x0025CB07
		// (set) Token: 0x06005C40 RID: 23616 RVA: 0x0025E90F File Offset: 0x0025CB0F
		public Token NameToken { get; private set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x0025E918 File Offset: 0x0025CB18
		// (set) Token: 0x06005C42 RID: 23618 RVA: 0x0025E920 File Offset: 0x0025CB20
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0025E929 File Offset: 0x0025CB29
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0025E931 File Offset: 0x0025CB31
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x06005C45 RID: 23621 RVA: 0x0025E93C File Offset: 0x0025CB3C
		public LabelStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.NameToken = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.SourceRef = this.NameToken.GetSourceRef(true);
			this.Label = this.NameToken.Text;
			lcontext.Scope.DefineLabel(this);
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x0025E9A9 File Offset: 0x0025CBA9
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x0025E9B9 File Offset: 0x0025CBB9
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Gotos.Add(gotostat);
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x0025E9C8 File Offset: 0x0025CBC8
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Clean(this.m_StackFrame);
			this.Address = bc.GetJumpPointForLastInstruction();
			foreach (GotoStatement gotoStatement in this.m_Gotos)
			{
				gotoStatement.SetAddress(this.Address);
			}
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x0025EA38 File Offset: 0x0025CC38
		internal void SetScope(RuntimeScopeBlock runtimeScopeBlock)
		{
			this.m_StackFrame = runtimeScopeBlock;
		}

		// Token: 0x04005382 RID: 21378
		private List<GotoStatement> m_Gotos = new List<GotoStatement>();

		// Token: 0x04005383 RID: 21379
		private RuntimeScopeBlock m_StackFrame;
	}
}
