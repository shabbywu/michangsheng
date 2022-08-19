using System;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CCD RID: 3277
	internal class LoopBoundary : ILoop
	{
		// Token: 0x06005BFD RID: 23549 RVA: 0x0025D144 File Offset: 0x0025B344
		public void CompileBreak(ByteCode bc)
		{
			throw new InternalErrorException("CompileBreak called on LoopBoundary");
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00024C5F File Offset: 0x00022E5F
		public bool IsBoundary()
		{
			return true;
		}
	}
}
