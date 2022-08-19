using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD7 RID: 3287
	internal class FunctionCallStatement : Statement
	{
		// Token: 0x06005C20 RID: 23584 RVA: 0x0025E040 File Offset: 0x0025C240
		public FunctionCallStatement(ScriptLoadingContext lcontext, FunctionCallExpression functionCallExpression) : base(lcontext)
		{
			this.m_FunctionCallExpression = functionCallExpression;
			lcontext.Source.Refs.Add(this.m_FunctionCallExpression.SourceRef);
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x0025E06C File Offset: 0x0025C26C
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_FunctionCallExpression.SourceRef))
			{
				this.m_FunctionCallExpression.Compile(bc);
				this.RemoveBreakpointStop(bc.Emit_Pop(1));
			}
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x0025E0C0 File Offset: 0x0025C2C0
		private void RemoveBreakpointStop(Instruction instruction)
		{
			instruction.SourceCodeRef = null;
		}

		// Token: 0x04005369 RID: 21353
		private FunctionCallExpression m_FunctionCallExpression;
	}
}
