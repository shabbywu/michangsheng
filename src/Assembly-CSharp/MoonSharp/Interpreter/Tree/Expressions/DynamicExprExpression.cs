using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Expressions
{
	// Token: 0x02000CE3 RID: 3299
	internal class DynamicExprExpression : Expression
	{
		// Token: 0x06005C6B RID: 23659 RVA: 0x0025FB5A File Offset: 0x0025DD5A
		public DynamicExprExpression(Expression exp, ScriptLoadingContext lcontext) : base(lcontext)
		{
			lcontext.Anonymous = true;
			this.m_Exp = exp;
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0025FB71 File Offset: 0x0025DD71
		public override DynValue Eval(ScriptExecutionContext context)
		{
			return this.m_Exp.Eval(context);
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0025FB7F File Offset: 0x0025DD7F
		public override void Compile(ByteCode bc)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x0025FB86 File Offset: 0x0025DD86
		public override SymbolRef FindDynamic(ScriptExecutionContext context)
		{
			return this.m_Exp.FindDynamic(context);
		}

		// Token: 0x0400539F RID: 21407
		private Expression m_Exp;
	}
}
