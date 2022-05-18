using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B2 RID: 4274
	internal class IfStatement : Statement
	{
		// Token: 0x06006745 RID: 26437 RVA: 0x00287C44 File Offset: 0x00285E44
		public IfStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			while (lcontext.Lexer.Current.Type != TokenType.Else && lcontext.Lexer.Current.Type != TokenType.End)
			{
				this.m_Ifs.Add(this.CreateIfBlock(lcontext));
			}
			if (lcontext.Lexer.Current.Type == TokenType.Else)
			{
				this.m_Else = this.CreateElseBlock(lcontext);
			}
			this.m_End = NodeBase.CheckTokenType(lcontext, TokenType.End).GetSourceRef(true);
			lcontext.Source.Refs.Add(this.m_End);
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x00287CE8 File Offset: 0x00285EE8
		private IfStatement.IfBlock CreateIfBlock(ScriptLoadingContext lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.If, TokenType.ElseIf);
			lcontext.Scope.PushBlock();
			IfStatement.IfBlock ifBlock = new IfStatement.IfBlock();
			ifBlock.Exp = Expression.Expr(lcontext);
			ifBlock.Source = token.GetSourceRef(NodeBase.CheckTokenType(lcontext, TokenType.Then), true);
			ifBlock.Block = new CompositeStatement(lcontext);
			ifBlock.StackFrame = lcontext.Scope.PopBlock();
			lcontext.Source.Refs.Add(ifBlock.Source);
			return ifBlock;
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x00287D68 File Offset: 0x00285F68
		private IfStatement.IfBlock CreateElseBlock(ScriptLoadingContext lcontext)
		{
			Token token = NodeBase.CheckTokenType(lcontext, TokenType.Else);
			lcontext.Scope.PushBlock();
			IfStatement.IfBlock ifBlock = new IfStatement.IfBlock();
			ifBlock.Block = new CompositeStatement(lcontext);
			ifBlock.StackFrame = lcontext.Scope.PopBlock();
			ifBlock.Source = token.GetSourceRef(true);
			lcontext.Source.Refs.Add(ifBlock.Source);
			return ifBlock;
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x00287DD0 File Offset: 0x00285FD0
		public override void Compile(ByteCode bc)
		{
			List<Instruction> list = new List<Instruction>();
			Instruction instruction = null;
			foreach (IfStatement.IfBlock ifBlock in this.m_Ifs)
			{
				using (bc.EnterSource(ifBlock.Source))
				{
					if (instruction != null)
					{
						instruction.NumVal = bc.GetJumpPointForNextInstruction();
					}
					ifBlock.Exp.Compile(bc);
					instruction = bc.Emit_Jump(OpCode.Jf, -1, 0);
					bc.Emit_Enter(ifBlock.StackFrame);
					ifBlock.Block.Compile(bc);
				}
				using (bc.EnterSource(this.m_End))
				{
					bc.Emit_Leave(ifBlock.StackFrame);
				}
				list.Add(bc.Emit_Jump(OpCode.Jump, -1, 0));
			}
			instruction.NumVal = bc.GetJumpPointForNextInstruction();
			if (this.m_Else != null)
			{
				using (bc.EnterSource(this.m_Else.Source))
				{
					bc.Emit_Enter(this.m_Else.StackFrame);
					this.m_Else.Block.Compile(bc);
				}
				using (bc.EnterSource(this.m_End))
				{
					bc.Emit_Leave(this.m_Else.StackFrame);
				}
			}
			foreach (Instruction instruction2 in list)
			{
				instruction2.NumVal = bc.GetJumpPointForNextInstruction();
			}
		}

		// Token: 0x04005F5F RID: 24415
		private List<IfStatement.IfBlock> m_Ifs = new List<IfStatement.IfBlock>();

		// Token: 0x04005F60 RID: 24416
		private IfStatement.IfBlock m_Else;

		// Token: 0x04005F61 RID: 24417
		private SourceRef m_End;

		// Token: 0x020010B3 RID: 4275
		private class IfBlock
		{
			// Token: 0x04005F62 RID: 24418
			public Expression Exp;

			// Token: 0x04005F63 RID: 24419
			public Statement Block;

			// Token: 0x04005F64 RID: 24420
			public RuntimeScopeBlock StackFrame;

			// Token: 0x04005F65 RID: 24421
			public SourceRef Source;
		}
	}
}
