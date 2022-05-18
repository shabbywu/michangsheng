using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;
using MoonSharp.Interpreter.Tree.Expressions;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010AE RID: 4270
	internal class FunctionCallStatement : Statement
	{
		// Token: 0x0600672C RID: 26412 RVA: 0x0004702C File Offset: 0x0004522C
		public FunctionCallStatement(ScriptLoadingContext lcontext, FunctionCallExpression functionCallExpression) : base(lcontext)
		{
			this.m_FunctionCallExpression = functionCallExpression;
			lcontext.Source.Refs.Add(this.m_FunctionCallExpression.SourceRef);
		}

		// Token: 0x0600672D RID: 26413 RVA: 0x00287810 File Offset: 0x00285A10
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_FunctionCallExpression.SourceRef))
			{
				this.m_FunctionCallExpression.Compile(bc);
				this.RemoveBreakpointStop(bc.Emit_Pop(1));
			}
		}

		// Token: 0x0600672E RID: 26414 RVA: 0x00047057 File Offset: 0x00045257
		private void RemoveBreakpointStop(Instruction instruction)
		{
			instruction.SourceCodeRef = null;
		}

		// Token: 0x04005F4D RID: 24397
		private FunctionCallExpression m_FunctionCallExpression;
	}
}
