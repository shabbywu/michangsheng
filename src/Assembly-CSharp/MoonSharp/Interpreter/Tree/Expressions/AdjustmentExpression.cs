using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE1 RID: 3297
	internal class AdjustmentExpression : Expression
	{
		// Token: 0x06005C57 RID: 23639 RVA: 0x0025F260 File Offset: 0x0025D460
		public AdjustmentExpression(ScriptLoadingContext lcontext, Expression exp) : base(lcontext)
		{
			this.expression = exp;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x0025F270 File Offset: 0x0025D470
		public override void Compile(ByteCode bc)
		{
			this.expression.Compile(bc);
			bc.Emit_Scalar();
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x0025F285 File Offset: 0x0025D485
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.expression.Eval(context).ToScalar();
		}

		// Token: 0x04005394 RID: 21396
		private Expression expression;
	}
}
