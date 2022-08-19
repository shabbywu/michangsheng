using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE8 RID: 3304
	internal class LiteralExpression : Expression
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x0026066C File Offset: 0x0025E86C
		public DynValue Value
		{
			get
			{
				return this.m_Value;
			}
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00260674 File Offset: 0x0025E874
		public LiteralExpression(ScriptLoadingContext lcontext, DynValue value) : base(lcontext)
		{
			this.m_Value = value;
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x00260684 File Offset: 0x0025E884
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

		// Token: 0x06005C8C RID: 23692 RVA: 0x00260753 File Offset: 0x0025E953
		public override void Compile(ByteCode bc)
		{
			bc.Emit_Literal(this.m_Value);
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x0026066C File Offset: 0x0025E86C
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Value;
		}

		// Token: 0x040053B3 RID: 21427
		private DynValue m_Value;
	}
}
