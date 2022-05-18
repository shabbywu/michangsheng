using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x020010A7 RID: 4263
	internal class BreakStatement : Statement
	{
		// Token: 0x0600671C RID: 26396 RVA: 0x00046FD0 File Offset: 0x000451D0
		public BreakStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Ref = NodeBase.CheckTokenType(lcontext, TokenType.Break).GetSourceRef(true);
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x0600671D RID: 26397 RVA: 0x00286FC0 File Offset: 0x002851C0
		public override void Compile(ByteCode bc)
		{
			using (bc.EnterSource(this.m_Ref))
			{
				if (bc.LoopTracker.Loops.Count == 0)
				{
					throw new SyntaxErrorException(base.Script, this.m_Ref, "<break> at line {0} not inside a loop", new object[]
					{
						this.m_Ref.FromLine
					});
				}
				ILoop loop = bc.LoopTracker.Loops.Peek(0);
				if (loop.IsBoundary())
				{
					throw new SyntaxErrorException(base.Script, this.m_Ref, "<break> at line {0} not inside a loop", new object[]
					{
						this.m_Ref.FromLine
					});
				}
				loop.CompileBreak(bc);
			}
		}

		// Token: 0x04005F37 RID: 24375
		private SourceRef m_Ref;
	}
}
