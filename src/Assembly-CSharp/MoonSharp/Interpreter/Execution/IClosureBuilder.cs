using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115A RID: 4442
	internal interface IClosureBuilder
	{
		// Token: 0x06006BCB RID: 27595
		SymbolRef CreateUpvalue(BuildTimeScope scope, SymbolRef symbol);
	}
}
