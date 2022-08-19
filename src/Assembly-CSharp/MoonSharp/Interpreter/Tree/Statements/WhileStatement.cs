using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDF RID: 3295
	internal class WhileStatement : Statement
	{
		// Token: 0x06005C51 RID: 23633 RVA: 0x0025EE20 File Offset: 0x0025D020
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

		// Token: 0x06005C52 RID: 23634 RVA: 0x0025EED0 File Offset: 0x0025D0D0
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

		// Token: 0x0400538F RID: 21391
		private Expression m_Condition;

		// Token: 0x04005390 RID: 21392
		private Statement m_Block;

		// Token: 0x04005391 RID: 21393
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005392 RID: 21394
		private SourceRef m_Start;

		// Token: 0x04005393 RID: 21395
		private SourceRef m_End;
	}
}
