using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDA RID: 3290
	internal class IfStatement : Statement
	{
		// Token: 0x06005C35 RID: 23605 RVA: 0x0025E560 File Offset: 0x0025C760
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

		// Token: 0x06005C36 RID: 23606 RVA: 0x0025E604 File Offset: 0x0025C804
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

		// Token: 0x06005C37 RID: 23607 RVA: 0x0025E684 File Offset: 0x0025C884
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

		// Token: 0x06005C38 RID: 23608 RVA: 0x0025E6EC File Offset: 0x0025C8EC
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

		// Token: 0x04005379 RID: 21369
		private List<IfStatement.IfBlock> m_Ifs = new List<IfStatement.IfBlock>();

		// Token: 0x0400537A RID: 21370
		private IfStatement.IfBlock m_Else;

		// Token: 0x0400537B RID: 21371
		private SourceRef m_End;

		// Token: 0x0200164D RID: 5709
		private class IfBlock
		{
			// Token: 0x04007243 RID: 29251
			public Expression Exp;

			// Token: 0x04007244 RID: 29252
			public Statement Block;

			// Token: 0x04007245 RID: 29253
			public RuntimeScopeBlock StackFrame;

			// Token: 0x04007246 RID: 29254
			public SourceRef Source;
		}
	}
}
