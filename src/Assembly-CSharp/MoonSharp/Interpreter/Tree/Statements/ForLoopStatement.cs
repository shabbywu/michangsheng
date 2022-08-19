using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD6 RID: 3286
	internal class ForLoopStatement : Statement
	{
		// Token: 0x06005C1E RID: 23582 RVA: 0x0025DDD0 File Offset: 0x0025BFD0
		public ForLoopStatement(ScriptLoadingContext lcontext, Token nameToken, Token forToken) : base(lcontext)
		{
			NodeBase.CheckTokenType(lcontext, TokenType.Op_Assignment);
			this.m_Start = Expression.Expr(lcontext);
			NodeBase.CheckTokenType(lcontext, TokenType.Comma);
			this.m_End = Expression.Expr(lcontext);
			if (lcontext.Lexer.Current.Type == TokenType.Comma)
			{
				lcontext.Lexer.Next();
				this.m_Step = Expression.Expr(lcontext);
			}
			else
			{
				this.m_Step = new LiteralExpression(lcontext, DynValue.NewNumber(1.0));
			}
			lcontext.Scope.PushBlock();
			this.m_VarName = lcontext.Scope.DefineLocal(nameToken.Text);
			this.m_RefFor = forToken.GetSourceRef(NodeBase.CheckTokenType(lcontext, TokenType.Do), true);
			this.m_InnerBlock = new CompositeStatement(lcontext);
			this.m_RefEnd = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			this.m_StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(this.m_RefFor);
			lcontext.Source.Refs.Add(this.m_RefEnd);
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x0025DEE8 File Offset: 0x0025C0E8
		public override void Compile(ByteCode bc)
		{
			bc.PushSourceRef(this.m_RefFor);
			Loop loop = new Loop
			{
				Scope = this.m_StackFrame
			};
			bc.LoopTracker.Loops.Push(loop);
			this.m_End.Compile(bc);
			bc.Emit_ToNum(3);
			this.m_Step.Compile(bc);
			bc.Emit_ToNum(2);
			this.m_Start.Compile(bc);
			bc.Emit_ToNum(1);
			int jumpPointForNextInstruction = bc.GetJumpPointForNextInstruction();
			Instruction instruction = bc.Emit_Jump(OpCode.JFor, -1, 0);
			bc.Emit_Enter(this.m_StackFrame);
			bc.Emit_Store(this.m_VarName, 0, 0);
			this.m_InnerBlock.Compile(bc);
			bc.PopSourceRef();
			bc.PushSourceRef(this.m_RefEnd);
			bc.Emit_Leave(this.m_StackFrame);
			bc.Emit_Incr(1);
			bc.Emit_Jump(OpCode.Jump, jumpPointForNextInstruction, 0);
			bc.LoopTracker.Loops.Pop();
			int jumpPointForNextInstruction2 = bc.GetJumpPointForNextInstruction();
			foreach (Instruction instruction2 in loop.BreakJumps)
			{
				instruction2.NumVal = jumpPointForNextInstruction2;
			}
			instruction.NumVal = jumpPointForNextInstruction2;
			bc.Emit_Pop(3);
			bc.PopSourceRef();
		}

		// Token: 0x04005361 RID: 21345
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005362 RID: 21346
		private Statement m_InnerBlock;

		// Token: 0x04005363 RID: 21347
		private SymbolRef m_VarName;

		// Token: 0x04005364 RID: 21348
		private Expression m_Start;

		// Token: 0x04005365 RID: 21349
		private Expression m_End;

		// Token: 0x04005366 RID: 21350
		private Expression m_Step;

		// Token: 0x04005367 RID: 21351
		private SourceRef m_RefFor;

		// Token: 0x04005368 RID: 21352
		private SourceRef m_RefEnd;
	}
}
