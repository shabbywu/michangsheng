using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010B6 RID: 4278
	internal class ReturnStatement : Statement
	{
		// Token: 0x0600675D RID: 26461 RVA: 0x000471DB File Offset: 0x000453DB
		public ReturnStatement(ScriptLoadingContext lcontext, Expression e, SourceRef sref) : base(lcontext)
		{
			this.m_Expression = e;
			this.m_Ref = sref;
			lcontext.Source.Refs.Add(sref);
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x0028823C File Offset: 0x0028643C
		public ReturnStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			Token token = lcontext.Lexer.Current;
			lcontext.Lexer.Next();
			Token token2 = lcontext.Lexer.Current;
			if (token2.IsEndOfBlock() || token2.Type == TokenType.SemiColon)
			{
				this.m_Expression = null;
				this.m_Ref = token.GetSourceRef(true);
			}
			else
			{
				this.m_Expression = new ExprListExpression(Expression.ExprList(lcontext), lcontext);
				this.m_Ref = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			}
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x002882DC File Offset: 0x002864DC
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Ref))
			{
				if (this.m_Expression != null)
				{
					this.m_Expression.Compile(bc);
					bc.Emit_Ret(1);
				}
				else
				{
					bc.Emit_Ret(0);
				}
			}
		}

		// Token: 0x04005F73 RID: 24435
		private Expression m_Expression;

		// Token: 0x04005F74 RID: 24436
		private SourceRef m_Ref;
	}
}
