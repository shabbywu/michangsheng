using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C5 RID: 4293
	internal class LiteralExpression : Expression
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600679F RID: 26527 RVA: 0x000473E6 File Offset: 0x000455E6
		public DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x000473EE File Offset: 0x000455EE
		public LiteralExpression(ScriptLoadingContext lcontext, DynValue value) : base(lcontext)
		{
			this.m_Value = value;
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x00289ABC File Offset: 0x00287CBC
		public LiteralExpression(ScriptLoadingContext lcontext, Token t) : base(lcontext)
		{
			TokenType type = t.Type;
			if (type <= TokenType.Nil)
			{
				if (type == TokenType.False)
				{
					this.m_Value = DynValue.False;
					goto IL_94;
				}
				if (type == TokenType.Nil)
				{
					this.m_Value = DynValue.Nil;
					goto IL_94;
				}
			}
			else
			{
				if (type == TokenType.True)
				{
					this.m_Value = DynValue.True;
					goto IL_94;
				}
				if (type - TokenType.String <= 1)
				{
					this.m_Value = DynValue.NewString(t.Text).AsReadOnly();
					goto IL_94;
				}
				if (type - TokenType.Number <= 2)
				{
					this.m_Value = DynValue.NewNumber(t.GetNumberValue()).AsReadOnly();
					goto IL_94;
				}
			}
			throw new InternalErrorException("type mismatch");
			IL_94:
			if (this.m_Value == null)
			{
				throw new SyntaxErrorException(t, "unknown literal format near '{0}'", new object[]
				{
					t.Text
				});
			}
			lcontext.Lexer.Next();
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x000473FE File Offset: 0x000455FE
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Literal(this.m_Value);
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x000473E6 File Offset: 0x000455E6
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Value;
		}

		// Token: 0x04005FB7 RID: 24503
		private DynValue m_Value;
	}
}
