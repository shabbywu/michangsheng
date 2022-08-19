using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4B RID: 3403
	internal interface IClosureBuilder
	{
		// Token: 0x06005FE4 RID: 24548
		SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol);
	}
}
