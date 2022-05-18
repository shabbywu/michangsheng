using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C7 RID: 4295
	internal class TableConstructor : Expression
	{
		// Token: 0x060067AA RID: 26538 RVA: 0x00289C24 File Offset: 0x00287E24
		public TableConstructor(ScriptLoadingContext lcontext, bool shared) : base(lcontext)
		{
			this.m_Shared = shared;
			NodeBase.CheckTokenType(lcontext, TokenType.Brk_Open_Curly, TokenType.Brk_Open_Curly_Shared);
			while (lcontext.Lexer.Current.Type != TokenType.Brk_Close_Curly)
			{
				TokenType type = lcontext.Lexer.Current.Type;
				if (type != TokenType.Name)
				{
					if (type != TokenType.Brk_Open_Square)
					{
						this.ArrayField(lcontext);
					}
					else
					{
						this.MapField(lcontext);
					}
				}
				else if (lcontext.Lexer.PeekNext().Type == TokenType.Op_Assignment)
				{
					this.StructField(lcontext);
				}
				else
				{
					this.ArrayField(lcontext);
				}
				Token token = lcontext.Lexer.Current;
				if (token.Type != TokenType.Comma && token.Type != TokenType.SemiColon)
				{
					break;
				}
				lcontext.Lexer.Next();
			}
			NodeBase.CheckTokenType(lcontext, TokenType.Brk_Close_Curly);
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x00289D00 File Offset: 0x00287F00
		private void MapField(ScriptLoadingContext lcontext)
		{
			lcontext.Lexer.Next();
			Expression key = Expression.Expr(lcontext);
			NodeBase.CheckTokenType(lcontext, TokenType.Brk_Close_Square);
			NodeBase.CheckTokenType(lcontext, TokenType.Op_Assignment);
			Expression value = Expression.Expr(lcontext);
			this.m_CtorArgs.Add(new KeyValuePair<Expression, Expression>(key, value));
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x00289D4C File Offset: 0x00287F4C
		private void StructField(ScriptLoadingContext lcontext)
		{
			Expression key = new LiteralExpression(lcontext, DynValue.NewString(lcontext.Lexer.Current.Text));
			lcontext.Lexer.Next();
			NodeBase.CheckTokenType(lcontext, TokenType.Op_Assignment);
			Expression value = Expression.Expr(lcontext);
			this.m_CtorArgs.Add(new KeyValuePair<Expression, Expression>(key, value));
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x00289DA4 File Offset: 0x00287FA4
		private void ArrayField(ScriptLoadingContext lcontext)
		{
			Expression item = Expression.Expr(lcontext);
			this.m_PositionalValues.Add(item);
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x00289DC4 File Offset: 0x00287FC4
		public override void Compile(ByteCode bc)
		{
			bc.Emit_NewTable(this.m_Shared);
			foreach (KeyValuePair<Expression, Expression> keyValuePair in this.m_CtorArgs)
			{
				keyValuePair.Key.Compile(bc);
				keyValuePair.Value.Compile(bc);
				bc.Emit_TblInitN();
			}
			for (int i = 0; i < this.m_PositionalValues.Count; i++)
			{
				this.m_PositionalValues[i].Compile(bc);
				bc.Emit_TblInitI(i == this.m_PositionalValues.Count - 1);
			}
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x00289E80 File Offset: 0x00288080
		public override DynValue Eval(ScriptExecutionContext context)
		{
			if (!this.m_Shared)
			{
				throw new DynamicExpressionException("Dynamic Expressions cannot define new non-prime tables.");
			}
			DynValue dynValue = DynValue.NewPrimeTable();
			Table table = dynValue.Table;
			int num = 0;
			foreach (Expression expression in this.m_PositionalValues)
			{
				table.Set(++num, expression.Eval(context));
			}
			foreach (KeyValuePair<Expression, Expression> keyValuePair in this.m_CtorArgs)
			{
				table.Set(keyValuePair.Key.Eval(context), keyValuePair.Value.Eval(context));
			}
			return dynValue;
		}

		// Token: 0x04005FBA RID: 24506
		private bool m_Shared;

		// Token: 0x04005FBB RID: 24507
		private List<Expression> m_PositionalValues = new List<Expression>();

		// Token: 0x04005FBC RID: 24508
		private List<KeyValuePair<Expression, Expression>> m_CtorArgs = new List<KeyValuePair<Expression, Expression>>();
	}
}
