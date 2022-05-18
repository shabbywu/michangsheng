using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B7 RID: 4279
	internal class ScopeBlockStatement : Statement
	{
		// Token: 0x06006760 RID: 26464 RVA: 0x00288338 File Offset: 0x00286538
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

		// Token: 0x06006761 RID: 26465 RVA: 0x002883C8 File Offset: 0x002865C8
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

		// Token: 0x04005F75 RID: 24437
		private Statement m_Block;

		// Token: 0x04005F76 RID: 24438
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005F77 RID: 24439
		private SourceRef m_Do;

		// Token: 0x04005F78 RID: 24440
		private SourceRef m_End;
	}
}
