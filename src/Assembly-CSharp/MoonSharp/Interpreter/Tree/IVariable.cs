using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x0200109D RID: 4253
	internal interface IVariable
	{
		// Token: 0x060066D5 RID: 26325
		void CompileAssignment(ByteCode bc, int stackofs, int tupleidx);
	}
}
