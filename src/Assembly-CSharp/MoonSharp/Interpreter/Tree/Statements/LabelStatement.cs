using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B4 RID: 4276
	internal class LabelStatement : Statement
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x0004714E File Offset: 0x0004534E
		// (set) Token: 0x0600674B RID: 26443 RVA: 0x00047156 File Offset: 0x00045356
		public string Label { get; private set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x0004715F File Offset: 0x0004535F
		// (set) Token: 0x0600674D RID: 26445 RVA: 0x00047167 File Offset: 0x00045367
		public int Address { get; private set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x00047170 File Offset: 0x00045370
		// (set) Token: 0x0600674F RID: 26447 RVA: 0x00047178 File Offset: 0x00045378
		public SourceRef SourceRef { get; private set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x00047181 File Offset: 0x00045381
		// (set) Token: 0x06006751 RID: 26449 RVA: 0x00047189 File Offset: 0x00045389
		public Token NameToken { get; private set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x00047192 File Offset: 0x00045392
		// (set) Token: 0x06006753 RID: 26451 RVA: 0x0004719A File Offset: 0x0004539A
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06006754 RID: 26452 RVA: 0x000471A3 File Offset: 0x000453A3
		// (set) Token: 0x06006755 RID: 26453 RVA: 0x000471AB File Offset: 0x000453AB
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x06006756 RID: 26454 RVA: 0x00287FB8 File Offset: 0x002861B8
		public LabelStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.NameToken = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			NodeBase.CheckTokenType(lcontext, TokenType.DoubleColon);
			this.SourceRef = this.NameToken.GetSourceRef(true);
			this.Label = this.NameToken.Text;
			lcontext.Scope.DefineLabel(this);
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x000471B4 File Offset: 0x000453B4
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x000471C4 File Offset: 0x000453C4
		internal void RegisterGoto(GotoStatement gotostat)
		{
			this.m_Gotos.Add(gotostat);
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00288028 File Offset: 0x00286228
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Clean(this.m_StackFrame);
			this.Address = bc.GetJumpPointForLastInstruction();
			foreach (GotoStatement gotoStatement in this.m_Gotos)
			{
				gotoStatement.SetAddress(this.Address);
			}
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x000471D2 File Offset: 0x000453D2
		internal void SetScope(RuntimeScopeBlock runtimeScopeBlock)
		{
			this.m_StackFrame = runtimeScopeBlock;
		}

		// Token: 0x04005F6C RID: 24428
		private List<GotoStatement> m_Gotos = new List<GotoStatement>();

		// Token: 0x04005F6D RID: 24429
		private RuntimeScopeBlock m_StackFrame;
	}
}
