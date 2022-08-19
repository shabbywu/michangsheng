using System;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CC7 RID: 3271
	internal interface IVariable
	{
		// Token: 0x06005BCC RID: 23500
		void CompileAssignment(ByteCode bc, int stackofs, int tupleidx);
	}
}
