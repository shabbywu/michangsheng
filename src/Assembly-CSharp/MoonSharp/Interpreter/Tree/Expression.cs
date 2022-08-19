using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CC6 RID: 3270
	internal abstract class Expression : NodeBase
	{
		// Token: 0x06005BC1 RID: 23489 RVA: 0x0025B2E1 File Offset: 0x002594E1
		public Expression(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual string GetFriendlyDebugName()
		{
			return null;
		}

		// Token: 0x06005BC3 RID: 23491
		public abstract DynValue Eval(ScriptExecutionContext context);

		// Token: 0x06005BC4 RID: 23492 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return null;
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0025B2EC File Offset: 0x002594EC
		internal static List<Expression> ExprListAfterFirstExpr(ScriptLoadingContext lcontext, Expression expr1)
		{
			List<Expression> list = new List<Expression>();
			list.Add(expr1);
			while (lcontext.Lexer.Current.Type == TokenType.Comma)
			{
				lcontext.Lexer.Next();
				list.Add(Expression.Expr(lcontext));
			}
			return list;
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0025B334 File Offset: 0x00259534
		internal static List<Expression> ExprList(ScriptLoadingContext lcontext)
		{
			List<Expression> list = new List<Expression>();
			for (;;)
			{
				list.Add(Expression.Expr(lcontext));
				if (lcontext.Lexer.Current.Type != TokenType.Comma)
				{
					break;
				}
				lcontext.Lexer.Next();
			}
			return list;
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0025B375 File Offset: 0x00259575
		internal static Expression Expr(ScriptLoadingContext lcontext)
		{
			return Expression.SubExpr(lcontext, true);
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0025B380 File Offset: 0x00259580
		internal static Expression SubExpr(ScriptLoadingContext lcontext, bool isPrimary)
		{
			Token token = lcontext.Lexer.Current;
			Expression expression;
			if (token.IsUnaryOperator())
			{
				lcontext.Lexer.Next();
				expression = Expression.SubExpr(lcontext, false);
				Token unaryOpToken = token;
				token = lcontext.Lexer.Current;
				if (isPrimary && token.Type == TokenType.Op_Pwr)
				{
					List<Expression> list = new List<Expression>();
					list.Add(expression);
					while (isPrimary && token.Type == TokenType.Op_Pwr)
					{
						lcontext.Lexer.Next();
						list.Add(Expression.SubExpr(lcontext, false));
						token = lcontext.Lexer.Current;
					}
					expression = list[list.Count - 1];
					for (int i = list.Count - 2; i >= 0; i--)
					{
						expression = BinaryOperatorExpression.CreatePowerExpression(list[i], expression, lcontext);
					}
				}
				expression = new UnaryOperatorExpression(lcontext, expression, unaryOpToken);
			}
			else
			{
				expression = Expression.SimpleExp(lcontext);
			}
			token = lcontext.Lexer.Current;
			if (isPrimary && token.IsBinaryOperator())
			{
				object chain = BinaryOperatorExpression.BeginOperatorChain();
				BinaryOperatorExpression.AddExpressionToChain(chain, expression);
				while (token.IsBinaryOperator())
				{
					BinaryOperatorExpression.AddOperatorToChain(chain, token);
					lcontext.Lexer.Next();
					Expression exp = Expression.SubExpr(lcontext, false);
					BinaryOperatorExpression.AddExpressionToChain(chain, exp);
					token = lcontext.Lexer.Current;
				}
				expression = BinaryOperatorExpression.CommitOperatorChain(chain, lcontext);
			}
			return expression;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x0025B4CC File Offset: 0x002596CC
		internal static Expression SimpleExp(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			TokenType type = token.Type;
			if (type > TokenType.True)
			{
				if (type <= TokenType.Brk_Open_Curly)
				{
					if (type == TokenType.VarArgs)
					{
						return new SymbolRefExpression(token, lcontext);
					}
					if (type != TokenType.Brk_Open_Curly)
					{
						goto IL_9A;
					}
				}
				else
				{
					if (type - TokenType.String <= 4)
					{
						goto IL_5C;
					}
					if (type != TokenType.Brk_Open_Curly_Shared)
					{
						goto IL_9A;
					}
				}
				return new TableConstructor(lcontext, token.Type == TokenType.Brk_Open_Curly_Shared);
			}
			switch (type)
			{
			case TokenType.False:
				break;
			case TokenType.For:
				goto IL_9A;
			case TokenType.Function:
				lcontext.Lexer.Next();
				return new FunctionDefinitionExpression(lcontext, false, false);
			case TokenType.Lambda:
				return new FunctionDefinitionExpression(lcontext, false, true);
			default:
				if (type != TokenType.Nil && type != TokenType.True)
				{
					goto IL_9A;
				}
				break;
			}
			IL_5C:
			return new LiteralExpression(lcontext, token);
			IL_9A:
			return Expression.PrimaryExp(lcontext);
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x0025B57C File Offset: 0x0025977C
		internal static Expression PrimaryExp(ScriptLoadingContext lcontext)
		{
			Expression expression = Expression.PrefixExp(lcontext);
			for (;;)
			{
				Token token = lcontext.Lexer.Current;
				Token thisCallName = null;
				TokenType type = token.Type;
				if (type <= TokenType.Colon)
				{
					if (type == TokenType.Dot)
					{
						lcontext.Lexer.Next();
						Token token2 = NodeBase.CheckTokenType(lcontext, TokenType.Name);
						expression = new IndexExpression(expression, token2.Text, lcontext);
						continue;
					}
					if (type != TokenType.Colon)
					{
						break;
					}
					lcontext.Lexer.Next();
					thisCallName = NodeBase.CheckTokenType(lcontext, TokenType.Name);
				}
				else
				{
					switch (type)
					{
					case TokenType.Brk_Open_Curly:
					case TokenType.Brk_Open_Round:
						break;
					case TokenType.Brk_Close_Round:
					case TokenType.Brk_Close_Square:
						return expression;
					case TokenType.Brk_Open_Square:
					{
						Token originalToken = lcontext.Lexer.Current;
						lcontext.Lexer.Next();
						Expression expression2 = Expression.Expr(lcontext);
						if (lcontext.Lexer.Current.Type == TokenType.Comma)
						{
							expression2 = new ExprListExpression(Expression.ExprListAfterFirstExpr(lcontext, expression2), lcontext);
						}
						NodeBase.CheckMatch(lcontext, originalToken, TokenType.Brk_Close_Square, "]");
						expression = new IndexExpression(expression, expression2, lcontext);
						continue;
					}
					default:
						if (type - TokenType.String > 1 && type != TokenType.Brk_Open_Curly_Shared)
						{
							return expression;
						}
						break;
					}
				}
				expression = new FunctionCallExpression(lcontext, expression, thisCallName);
			}
			return expression;
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0025B69C File Offset: 0x0025989C
		private static Expression PrefixExp(ScriptLoadingContext lcontext)
		{
			Token token = lcontext.Lexer.Current;
			TokenType type = token.Type;
			if (type == TokenType.Name)
			{
				return new SymbolRefExpression(token, lcontext);
			}
			if (type == TokenType.Brk_Open_Round)
			{
				lcontext.Lexer.Next();
				Expression expression = Expression.Expr(lcontext);
				expression = new AdjustmentExpression(lcontext, expression);
				NodeBase.CheckMatch(lcontext, token, TokenType.Brk_Close_Round, ")");
				return expression;
			}
			throw new SyntaxErrorException(token, "unexpected symbol near '{0}'", new object[]
			{
				token.Text
			})
			{
				IsPrematureStreamTermination = (token.Type == TokenType.Eof)
			};
		}
	}
}
