using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDC RID: 3292
	internal class RepeatStatement : Statement
	{
		// Token: 0x06005C4A RID: 23626 RVA: 0x0025EA44 File Offset: 0x0025CC44
		public RepeatStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Repeat = NodeBase.CheckTokenType(lcontext, TokenType.Repeat).GetSourceRef(true);
			lcontext.Scope.PushBlock();
			this.m_Block = new CompositeStatement(lcontext);
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Until);
			this.m_Condition = Expression.Expr(lcontext);
			this.m_Until = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Repeat);
			lcontext.Source.Refs.Add(this.m_Until);
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x0025EAF0 File Offset: 0x0025CCF0
		public override void Compile(ByteCode bc)
		{
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.PushSourceRef(this.m_Repeat);
			bc.LoopTracker.Loops.Push(loop);
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			bc.Emit_Enter(this.m_StackFrame);
			this.m_Block.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_Until);
			this.m_Condition.Compile(bc);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Jump(OpCode.Jf, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			foreach (Instruction instruction in loop.BreakJumps)
			{
				instruction.NumVal = jumpPointForNextInstruction2;
			}
			bc.PopSourceRef();
		}

		// Token: 0x04005384 RID: 21380
		private Expression m_Condition;

		// Token: 0x04005385 RID: 21381
		private Statement m_Block;

		// Token: 0x04005386 RID: 21382
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005387 RID: 21383
		private SourceRef m_Repeat;

		// Token: 0x04005388 RID: 21384
		private SourceRef m_Until;
	}
}
