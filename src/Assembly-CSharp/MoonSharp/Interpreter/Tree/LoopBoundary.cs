using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020010A3 RID: 4259
	internal class LoopBoundary : ILoop
	{
		// Token: 0x06006706 RID: 26374 RVA: 0x00046F63 File Offset: 0x00045163
		public void CompileBreak(ByteCode bc)
		{
			throw new InternalErrorException("CompileBreak called on LoopBoundary");
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x0000A093 File Offset: 0x00008293
		public bool IsBoundary()
		{
			return true;
		}
	}
}
