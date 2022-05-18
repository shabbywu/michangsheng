using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C1 RID: 4289
	internal class FunctionCallExpression : Expression
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06006786 RID: 26502 RVA: 0x00047341 File Offset: 0x00045541
		// (set) Token: 0x06006787 RID: 26503 RVA: 0x00047349 File Offset: 0x00045549
		internal SourceRef SourceRef { get; private set; }

		// Token: 0x06006788 RID: 26504 RVA: 0x0028913C File Offset: 0x0028733C
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

		// Token: 0x06006789 RID: 26505 RVA: 0x002892FC File Offset: 0x002874FC
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

		// Token: 0x0600678A RID: 26506 RVA: 0x00047352 File Offset: 0x00045552
		public override DynValue Eval(ScriptExecutionContext context)
		{
			throw new DynamicExpressionException("Dynamic Expressions cannot call functions.");
		}

		// Token: 0x04005FA3 RID: 24483
		private List<Expression> m_Arguments;

		// Token: 0x04005FA4 RID: 24484
		private Expression m_Function;

		// Token: 0x04005FA5 RID: 24485
		private string m_Name;

		// Token: 0x04005FA6 RID: 24486
		private string m_DebugErr;
	}
}
