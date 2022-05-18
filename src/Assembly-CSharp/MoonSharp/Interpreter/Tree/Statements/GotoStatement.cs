using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B1 RID: 4273
	internal class GotoStatement : Statement
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x000470B5 File Offset: 0x000452B5
		// (set) Token: 0x06006738 RID: 26424 RVA: 0x000470BD File Offset: 0x000452BD
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06006739 RID: 26425 RVA: 0x000470C6 File Offset: 0x000452C6
		// (set) Token: 0x0600673A RID: 26426 RVA: 0x000470CE File Offset: 0x000452CE
		internal Token GotoToken { get; private set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600673B RID: 26427 RVA: 0x000470D7 File Offset: 0x000452D7
		// (set) Token: 0x0600673C RID: 26428 RVA: 0x000470DF File Offset: 0x000452DF
		public string Label { get; private set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600673D RID: 26429 RVA: 0x000470E8 File Offset: 0x000452E8
		// (set) Token: 0x0600673E RID: 26430 RVA: 0x000470F0 File Offset: 0x000452F0
		internal int DefinedVarsCount { get; private set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x000470F9 File Offset: 0x000452F9
		// (set) Token: 0x06006740 RID: 26432 RVA: 0x00047101 File Offset: 0x00045301
		internal string LastDefinedVarName { get; private set; }

		// Token: 0x06006741 RID: 26433 RVA: 0x00287BE8 File Offset: 0x00285DE8
		public GotoStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.GotoToken = NodeBase.CheckTokenType(lcontext, TokenType.Goto);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
			this.SourceRef = this.GotoToken.GetSourceRef(token, true);
			this.Label = token.Text;
			lcontext.Scope.RegisterGoto(this);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0004710A File Offset: 0x0004530A
		public override void Compile(ByteCode bc)
		{
			this.m_Jump = bc.Emit_Jump(OpCode.Jump, this.m_LabelAddress, 0);
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x00047121 File Offset: 0x00045321
		internal void SetDefinedVars(int definedVarsCount, string lastDefinedVarsName)
		{
			this.DefinedVarsCount = definedVarsCount;
			this.LastDefinedVarName = lastDefinedVarsName;
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x00047131 File Offset: 0x00045331
		internal void SetAddress(int labelAddress)
		{
			this.m_LabelAddress = labelAddress;
			if (this.m_Jump != null)
			{
				this.m_Jump.NumVal = labelAddress;
			}
		}

		// Token: 0x04005F5D RID: 24413
		private Instruction m_Jump;

		// Token: 0x04005F5E RID: 24414
		private int m_LabelAddress = -1;
	}
}
