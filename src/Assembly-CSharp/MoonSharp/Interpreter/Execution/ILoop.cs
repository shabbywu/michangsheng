using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4C RID: 3404
	internal interface ILoop
	{
		// Token: 0x06005FE5 RID: 24549
		void CompileBreak(ByteCode bc);

		// Token: 0x06005FE6 RID: 24550
		bool IsBoundary();
	}
}
