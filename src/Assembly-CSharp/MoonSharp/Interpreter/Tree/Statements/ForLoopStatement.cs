using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010AD RID: 4269
	internal class ForLoopStatement : Statement
	{
		// Token: 0x0600672A RID: 26410 RVA: 0x002875A0 File Offset: 0x002857A0
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

		// Token: 0x0600672B RID: 26411 RVA: 0x002876B8 File Offset: 0x002858B8
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

		// Token: 0x04005F45 RID: 24389
		private RuntimeScopeBlock m_StackFrame;

		// Token: 0x04005F46 RID: 24390
		private Statement m_InnerBlock;

		// Token: 0x04005F47 RID: 24391
		private SymbolRef m_VarName;

		// Token: 0x04005F48 RID: 24392
		private Expression m_Start;

		// Token: 0x04005F49 RID: 24393
		private Expression m_End;

		// Token: 0x04005F4A RID: 24394
		private Expression m_Step;

		// Token: 0x04005F4B RID: 24395
		private SourceRef m_RefFor;

		// Token: 0x04005F4C RID: 24396
		private SourceRef m_RefEnd;
	}
}
