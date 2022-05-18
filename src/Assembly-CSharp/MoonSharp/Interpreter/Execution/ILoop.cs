using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115B RID: 4443
	internal interface ILoop
	{
		// Token: 0x06006BCC RID: 27596
		void CompileBreak(ByteCode bc);

		// Token: 0x06006BCD RID: 27597
		bool IsBoundary();
	}
}
