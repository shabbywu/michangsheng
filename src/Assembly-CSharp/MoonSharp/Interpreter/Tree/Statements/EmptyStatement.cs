using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree.Statements
{
	// Token: 0x02000CD4 RID: 3284
	internal class EmptyStatement : Statement
	{
		// Token: 0x06005C1A RID: 23578 RVA: 0x0025DAB4 File Offset: 0x0025BCB4
		public EmptyStatement(ScriptLoadingContext lcontext) : base(lcontext)
		{
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00004095 File Offset: 0x00002295
		public override void Compile(ByteCode bc)
		{
		}
	}
}
