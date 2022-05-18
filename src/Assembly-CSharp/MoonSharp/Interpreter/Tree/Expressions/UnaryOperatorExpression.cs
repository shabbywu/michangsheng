using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C8 RID: 4296
	internal class UnaryOperatorExpression : Expression
	{
		// Token: 0x060067B0 RID: 26544 RVA: 0x0004746C File Offset: 0x0004566C
		public UnaryOperatorExpression(ScriptLoadingContext lcontext, Expression subExpression, Token unaryOpToken) : base(lcontext)
		{
			this.m_OpText = unaryOpToken.Text;
			this.m_Exp = subExpression;
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x00289F60 File Offset: 0x00288160
		public override void Compile(ByteCode bc)
		{
			this.m_Exp.Compile(bc);
			string opText = this.m_OpText;
			if (opText == "not")
			{
				bc.Emit_Operator(OpCode.Not);
				return;
			}
			if (opText == "#")
			{
				bc.Emit_Operator(OpCode.Len);
				return;
			}
			if (!(opText == "-"))
			{
				throw new InternalErrorException("Unexpected unary operator '{0}'", new object[]
				{
					this.m_OpText
				});
			}
			bc.Emit_Operator(OpCode.Neg);
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x00289FE0 File Offset: 0x002881E0
		public override DynValue Eval(ScriptExecutionContext context)
		{
			DynValue dynValue = this.m_Exp.Eval(context).ToScalar();
			string opText = this.m_OpText;
			if (opText == "not")
			{
				return DynValue.NewBoolean(!dynValue.CastToBool());
			}
			if (opText == "#")
			{
				return dynValue.GetLength();
			}
			if (!(opText == "-"))
			{
				throw new DynamicExpressionException("Unexpected unary operator '{0}'", new object[]
				{
					this.m_OpText
				});
			}
			double? num = dynValue.CastToNumber();
			if (num != null)
			{
				return DynValue.NewNumber(-num.Value);
			}
			throw new DynamicExpressionException("Attempt to perform arithmetic on non-numbers.");
		}

		// Token: 0x04005FBD RID: 24509
		private Expression m_Exp;

		// Token: 0x04005FBE RID: 24510
		private string m_OpText;
	}
}
