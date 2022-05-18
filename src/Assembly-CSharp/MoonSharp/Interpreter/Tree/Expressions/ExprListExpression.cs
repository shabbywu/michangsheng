using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010C0 RID: 4288
	internal class ExprListExpression : Expression
	{
		// Token: 0x06006782 RID: 26498 RVA: 0x000472FC File Offset: 0x000454FC
		public ExprListExpression(List<Expression> exps, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.expressions = exps;
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x0004730C File Offset: 0x0004550C
		public Expression[] GetExpressions()
		{
			return this.expressions.ToArray();
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x002890C8 File Offset: 0x002872C8
		public override void Compile(ByteCode bc)
		{
			foreach (Expression expression in this.expressions)
			{
				expression.Compile(bc);
			}
			if (this.expressions.Count > 1)
			{
				bc.Emit_MkTuple(this.expressions.Count);
			}
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x00047319 File Offset: 0x00045519
		public override DynValue Eval(ScriptExecutionContext context)
		{
			if (this.expressions.Count >= 1)
			{
				return this.expressions[0].Eval(context);
			}
			return DynValue.Void;
		}

		// Token: 0x04005FA2 RID: 24482
		private List<Expression> expressions;
	}
}
