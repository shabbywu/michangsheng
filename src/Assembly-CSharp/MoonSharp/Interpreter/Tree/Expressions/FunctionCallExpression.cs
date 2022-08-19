using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE5 RID: 3301
	internal class FunctionCallExpression : Expression
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x0025FC50 File Offset: 0x0025DE50
		// (set) Token: 0x06005C74 RID: 23668 RVA: 0x0025FC58 File Offset: 0x0025DE58
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x06005C75 RID: 23669 RVA: 0x0025FC64 File Offset: 0x0025DE64
		public FunctionCallExpression(ScriptLoadingContext lcontext, Expression function, Token thisCallName) : base(lcontext)
		{
			Token token = thisCallName ?? lcontext.Lexer.Current;
			this.m_Name = ((thisCallName != null) ? thisCallName.Text : null);
			this.m_DebugErr = function.GetFriendlyDebugName();
			this.m_Function = function;
			TokenType type = lcontext.Lexer.Current.Type;
			if (type <= TokenType.Brk_Open_Round)
			{
				if (type != TokenType.Brk_Open_Curly)
				{
					if (type != TokenType.Brk_Open_Round)
					{
						goto IL_183;
					}
					Token originalToken = lcontext.Lexer.Current;
					lcontext.Lexer.Next();
					Token token2 = lcontext.Lexer.Current;
					if (token2.Type == TokenType.Brk_Close_Round)
					{
						this.m_Arguments = new List<Expression>();
						this.SourceRef = token.GetSourceRef(token2, true);
						lcontext.Lexer.Next();
						return;
					}
					this.m_Arguments = Expression.ExprList(lcontext);
					this.SourceRef = token.GetSourceRef(NodeBase.CheckMatch(lcontext, originalToken, TokenType.Brk_Close_Round, ")"), true);
					return;
				}
			}
			else
			{
				if (type - TokenType.String <= 1)
				{
					this.m_Arguments = new List<Expression>();
					Expression item = new LiteralExpression(lcontext, lcontext.Lexer.Current);
					this.m_Arguments.Add(item);
					this.SourceRef = token.GetSourceRef(lcontext.Lexer.Current, true);
					return;
				}
				if (type != TokenType.Brk_Open_Curly_Shared)
				{
					goto IL_183;
				}
			}
			this.m_Arguments = new List<Expression>();
			this.m_Arguments.Add(new TableConstructor(lcontext, lcontext.Lexer.Current.Type == TokenType.Brk_Open_Curly_Shared));
			this.SourceRef = token.GetSourceRefUpTo(lcontext.Lexer.Current, true);
			return;
			IL_183:
			throw new SyntaxErrorException(lcontext.Lexer.Current, "function arguments expected")
			{
				IsPrematureStreamTermination = (lcontext.Lexer.Current.Type == TokenType.Eof)
			};
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x0025FE24 File Offset: 0x0025E024
		public override void Compile(ByteCode bc)
		{
			this.m_Function.Compile(bc);
			int num = this.m_Arguments.Count;
			if (!string.IsNullOrEmpty(this.m_Name))
			{
				bc.Emit_Copy(0);
				bc.Emit_Index(DynValue.NewString(this.m_Name), true, false);
				bc.Emit_Swap(0, 1);
				num++;
			}
			for (int i = 0; i < this.m_Arguments.Count; i++)
			{
				this.m_Arguments[i].Compile(bc);
			}
			if (!string.IsNullOrEmpty(this.m_Name))
			{
				bc.Emit_ThisCall(num, this.m_DebugErr);
				return;
			}
			bc.Emit_Call(num, this.m_DebugErr);
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x0025FECF File Offset: 0x0025E0CF
		public override DynValue Eval(ScriptExecutionContext context)
		{
			throw new DynamicExpressionException("Dynamic Expressions cannot call functions.");
		}

		// Token: 0x040053A1 RID: 21409
		private List<Expression> m_Arguments;

		// Token: 0x040053A2 RID: 21410
		private Expression m_Function;

		// Token: 0x040053A3 RID: 21411
		private string m_Name;

		// Token: 0x040053A4 RID: 21412
		private string m_DebugErr;
	}
}
