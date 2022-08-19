using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD9 RID: 3289
	internal class GotoStatement : Statement
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x0025E46A File Offset: 0x0025C66A
		// (set) Token: 0x06005C28 RID: 23592 RVA: 0x0025E472 File Offset: 0x0025C672
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x0025E47B File Offset: 0x0025C67B
		// (set) Token: 0x06005C2A RID: 23594 RVA: 0x0025E483 File Offset: 0x0025C683
		internal Token GotoToken { get; private set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06005C2B RID: 23595 RVA: 0x0025E48C File Offset: 0x0025C68C
		// (set) Token: 0x06005C2C RID: 23596 RVA: 0x0025E494 File Offset: 0x0025C694
		public string Label { get; private set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x0025E49D File Offset: 0x0025C69D
		// (set) Token: 0x06005C2E RID: 23598 RVA: 0x0025E4A5 File Offset: 0x0025C6A5
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x0025E4AE File Offset: 0x0025C6AE
		// (set) Token: 0x06005C30 RID: 23600 RVA: 0x0025E4B6 File Offset: 0x0025C6B6
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x06005C31 RID: 23601 RVA: 0x0025E4C0 File Offset: 0x0025C6C0
		public GotoStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.GotoToken = NodeBase.CheckTokenType(lcontext, TokenType.Goto);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			this.SourceRef = this.GotoToken.GetSourceRef(token, true);
			this.Label = token.Text;
			lcontext.Scope.RegisterGoto(this);
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x0025E51C File Offset: 0x0025C71C
		public override void Compile(ByteCode bc)
		{
			this.m_Jump = bc.Emit_Jump(OpCode.Jump, this.m_LabelAddress, 0);
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0025E533 File Offset: 0x0025C733
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x0025E543 File Offset: 0x0025C743
		internal void SetAddress(int labelAddress)
		{
			this.m_LabelAddress = labelAddress;
			if (this.m_Jump != null)
			{
				this.m_Jump.NumVal = labelAddress;
			}
		}

		// Token: 0x04005377 RID: 21367
		private Instruction m_Jump;

		// Token: 0x04005378 RID: 21368
		private int m_LabelAddress = -1;
	}
}
