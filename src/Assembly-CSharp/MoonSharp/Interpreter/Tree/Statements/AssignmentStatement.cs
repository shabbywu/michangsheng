using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD0 RID: 3280
	internal class AssignmentStatement : Statement
	{
		// Token: 0x06005C0F RID: 23567 RVA: 0x0025D4B8 File Offset: 0x0025B6B8
		public AssignmentStatement(ScriptLoadingContext lcontext, Token startToken) : base(lcontext)
		{
			List<string> list = new List<string>();
			for (;;)
			{
				Token token = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				list.Add(token.Text);
				if (lcontext.Lexer.Current.Type != TokenType.Comma)
				{
					break;
				}
				lcontext.Lexer.Next();
			}
			if (lcontext.Lexer.Current.Type == TokenType.Op_Assignment)
			{
				NodeBase.CheckTokenType(lcontext, TokenType.Op_Assignment);
				this.m_RValues = Expression.ExprList(lcontext);
			}
			else
			{
				this.m_RValues = new List<Expression>();
			}
			foreach (string name in list)
			{
				SymbolRef refr = lcontext.Scope.TryDefineLocal(name);
				SymbolRefExpression item = new SymbolRefExpression(lcontext, refr);
				this.m_LValues.Add(item);
			}
			Token to = lcontext.Lexer.Current;
			this.m_Ref = startToken.GetSourceRefUpTo(to, true);
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x0025D5DC File Offset: 0x0025B7DC
		public AssignmentStatement(ScriptLoadingContext lcontext, Expression firstExpression, Token first) : base(lcontext)
		{
			this.m_LValues.Add(this.CheckVar(lcontext, firstExpression));
			while (lcontext.Lexer.Current.Type == TokenType.Comma)
			{
				lcontext.Lexer.Next();
				Expression firstExpression2 = Expression.PrimaryExp(lcontext);
				this.m_LValues.Add(this.CheckVar(lcontext, firstExpression2));
			}
			NodeBase.CheckTokenType(lcontext, TokenType.Op_Assignment);
			this.m_RValues = Expression.ExprList(lcontext);
			Token to = lcontext.Lexer.Current;
			this.m_Ref = first.GetSourceRefUpTo(to, true);
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x0025D690 File Offset: 0x0025B890
		private IVariable CheckVar(ScriptLoadingContext lcontext, Expression firstExpression)
		{
			IVariable variable = firstExpression as IVariable;
			if (variable == null)
			{
				throw new SyntaxErrorException(lcontext.Lexer.Current, "unexpected symbol near '{0}' - not a l-value", new object[]
				{
					lcontext.Lexer.Current
				});
			}
			return variable;
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x0025D6D4 File Offset: 0x0025B8D4
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Ref))
			{
				foreach (Expression expression in this.m_RValues)
				{
					expression.Compile(bc);
				}
				for (int i = 0; i < this.m_LValues.Count; i++)
				{
					this.m_LValues[i].CompileAssignment(bc, Math.Max(this.m_RValues.Count - 1 - i, 0), i - Math.Min(i, this.m_RValues.Count - 1));
				}
				bc.Emit_Pop(this.m_RValues.Count);
			}
		}

		// Token: 0x04005351 RID: 21329
		private List<IVariable> m_LValues = new List<IVariable>();

		// Token: 0x04005352 RID: 21330
		private List<Expression> m_RValues;

		// Token: 0x04005353 RID: 21331
		private SourceRef m_Ref;
	}
}
