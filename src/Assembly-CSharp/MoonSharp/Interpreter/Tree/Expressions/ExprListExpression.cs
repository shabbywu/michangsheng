using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE4 RID: 3300
	internal class ExprListExpression : Expression
	{
		// Token: 0x06005C6F RID: 23663 RVA: 0x0025FB94 File Offset: 0x0025DD94
		public ExprListExpression(List<Expression> exps, ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.expressions = exps;
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x0025FBA4 File Offset: 0x0025DDA4
		public Expression[] GetExpressions()
		{
			return this.expressions.ToArray();
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0025FBB4 File Offset: 0x0025DDB4
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

		// Token: 0x06005C72 RID: 23666 RVA: 0x0025FC28 File Offset: 0x0025DE28
		public override DynValue Eval(ScriptExecutionContext context)
		{
			if (this.expressions.Count >= 1)
			{
				return this.expressions[0].Eval(context);
			}
			return DynValue.Void;
		}

		// Token: 0x040053A0 RID: 21408
		private List<Expression> expressions;
	}
}
