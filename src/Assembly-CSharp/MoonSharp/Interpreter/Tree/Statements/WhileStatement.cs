using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B8 RID: 4280
	internal class WhileStatement : Statement
	{
		// Token: 0x06006762 RID: 26466 RVA: 0x0028844C File Offset: 0x0028664C
		public WhileStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.While);
			this.m_Condition = Expression.Expr(lcontext);
			this.m_Start = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			lcontext.Scope.PushBlock();
			NodeBase.CheckTokenType(lcontext, TokenType.Do);
			this.m_Block = new CompositeStatement(lcontext);
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_Start);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x002884FC File Offset: 0x002866FC
		public override void Compile(ByteCode bc)
		{
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.LoopTracker.Loops.Push(loop);
			bc.PushSourceRef(this.m_Start);
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			this.m_Condition.Compile(bc);
			Instruction instruction = bc.Emit_Jump(OpCode.Jf, -1, 0);
			bc.Emit_Enter(this.m_StackFrame);
			this.m_Block.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_End);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Jump(OpCode.Jump, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			foreach (Instruction instruction2 in loop.BreakJumps)
			{
				instruction2.NumVal = jumpPointForNextInstruction2;
			}
			instruction.NumVal = jumpPointForNextInstruction2;
			bc.PopSourceRef();
		}

		// Token: 0x04005F79 RID: 24441
		private Expression m_Condition;

		// Token: 0x04005F7A RID: 24442
		private Statement m_Block;

		// Token: 0x04005F7B RID: 24443
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005F7C RID: 24444
		private SourceRef m_Start;

		// Token: 0x04005F7D RID: 24445
		private SourceRef m_End;
	}
}
