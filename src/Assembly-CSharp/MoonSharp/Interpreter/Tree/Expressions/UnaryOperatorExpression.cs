using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CEB RID: 3307
	internal class UnaryOperatorExpression : Expression
	{
		// Token: 0x06005C9A RID: 23706 RVA: 0x00260B98 File Offset: 0x0025ED98
		public UnaryOperatorExpression(ScriptLoadingContext lcontext, Expression subExpression, Token unaryOpToken) : base(lcontext)
		{
			this.m_OpText = unaryOpToken.Text;
			this.m_Exp = subExpression;
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x00260BB4 File Offset: 0x0025EDB4
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

		// Token: 0x06005C9C RID: 23708 RVA: 0x00260C34 File Offset: 0x0025EE34
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

		// Token: 0x040053B9 RID: 21433
		private Expression m_Exp;

		// Token: 0x040053BA RID: 21434
		private string m_OpText;
	}
}
