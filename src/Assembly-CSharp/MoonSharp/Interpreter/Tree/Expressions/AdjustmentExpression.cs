using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010BA RID: 4282
	internal class AdjustmentExpression : Expression
	{
		// Token: 0x06006768 RID: 26472 RVA: 0x00047235 File Offset: 0x00045435
		public AdjustmentExpression(ScriptLoadingContext lcontext, Expression exp) : base(lcontext)
		{
			this.expression = exp;
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x00047245 File Offset: 0x00045445
		public override void Compile(ByteCode bc)
		{
			this.expression.Compile(bc);
			bc.Emit_Scalar();
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x0004725A File Offset: 0x0004545A
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.expression.Eval(context).ToScalar();
		}

		// Token: 0x04005F7E RID: 24446
		private Expression expression;
	}
}
