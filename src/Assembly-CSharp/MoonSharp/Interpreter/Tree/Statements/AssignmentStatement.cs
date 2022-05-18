using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010A6 RID: 4262
	internal class AssignmentStatement : Statement
	{
		// Token: 0x06006718 RID: 26392 RVA: 0x00286CC8 File Offset: 0x00284EC8
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

		// Token: 0x06006719 RID: 26393 RVA: 0x00286DEC File Offset: 0x00284FEC
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

		// Token: 0x0600671A RID: 26394 RVA: 0x00286EA0 File Offset: 0x002850A0
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

		// Token: 0x0600671B RID: 26395 RVA: 0x00286EE4 File Offset: 0x002850E4
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

		// Token: 0x04005F34 RID: 24372
		private List<IVariable> m_LValues = new List<IVariable>();

		// Token: 0x04005F35 RID: 24373
		private List<Expression> m_RValues;

		// Token: 0x04005F36 RID: 24374
		private SourceRef m_Ref;
	}
}
