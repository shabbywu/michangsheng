using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDE RID: 3294
	internal class ScopeBlockStatement : Statement
	{
		// Token: 0x06005C4F RID: 23631 RVA: 0x0025ED0C File Offset: 0x0025CF0C
		public ScopeBlockStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Scope.PushBlock();
			this.m_Do = NodeBase.CheckTokenType(lcontext, TokenType.Do).GetSourceRef(true);
			this.m_Block = new CompositeStatement(lcontext);
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Do);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x0025ED9C File Offset: 0x0025CF9C
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Do))
			{
				bc.Emit_Enter(this.m_StackFrame);
			}
			this.m_Block.Compile(bc);
			using (bc.EnterSource(this.m_End))
			{
				bc.Emit_Leave(this.m_StackFrame);
			}
		}

		// Token: 0x0400538B RID: 21387
		private Statement m_Block;

		// Token: 0x0400538C RID: 21388
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x0400538D RID: 21389
		private SourceRef m_Do;

		// Token: 0x0400538E RID: 21390
		private SourceRef m_End;
	}
}
