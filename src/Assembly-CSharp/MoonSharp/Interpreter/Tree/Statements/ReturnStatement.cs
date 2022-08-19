using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CDD RID: 3293
	internal class ReturnStatement : Statement
	{
		// Token: 0x06005C4C RID: 23628 RVA: 0x0025EBE8 File Offset: 0x0025CDE8
		public ReturnStatement(ScriptLoadingContext lcontext, Expression e, SourceRef sref) : base(lcontext)
		{
			this.m_Expression = e;
			this.m_Ref = sref;
			lcontext.Source.Refs.Add(sref);
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x0025EC10 File Offset: 0x0025CE10
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

		// Token: 0x06005C4E RID: 23630 RVA: 0x0025ECB0 File Offset: 0x0025CEB0
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

		// Token: 0x04005389 RID: 21385
		private Expression m_Expression;

		// Token: 0x0400538A RID: 21386
		private SourceRef m_Ref;
	}
}
