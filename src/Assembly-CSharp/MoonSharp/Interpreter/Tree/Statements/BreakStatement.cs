using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD1 RID: 3281
	internal class BreakStatement : Statement
	{
		// Token: 0x06005C13 RID: 23571 RVA: 0x0025D7B0 File Offset: 0x0025B9B0
		public BreakStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
			this.m_Ref = NodeBase.CheckTokenType(lcontext, TokenType.Break).GetSourceRef(true);
			lcontext.Source.Refs.Add(this.m_Ref);
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x0025D7E4 File Offset: 0x0025B9E4
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

		// Token: 0x04005354 RID: 21332
		private SourceRef m_Ref;
	}
}
