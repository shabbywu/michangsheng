using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x020010BF RID: 4287
	internal class DynamicExprExpression : Expression
	{
		// Token: 0x0600677E RID: 26494 RVA: 0x000472C2 File Offset: 0x000454C2
		public DynamicExprExpression(Expression exp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Anonymous = true;
			this.m_Exp = exp;
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x000472D9 File Offset: 0x000454D9
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Exp.Eval(context);
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x000472E7 File Offset: 0x000454E7
		public override void Compile(ByteCode bc)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x000472EE File Offset: 0x000454EE
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return this.m_Exp.FindDynamic(context);
		}

		// Token: 0x04005FA1 RID: 24481
		private Expression m_Exp;
	}
}
