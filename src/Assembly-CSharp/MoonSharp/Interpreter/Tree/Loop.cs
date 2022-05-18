using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x020010A2 RID: 4258
	internal class Loop : ILoop
	{
		// Token: 0x06006703 RID: 26371 RVA: 0x00046F2C File Offset: 0x0004512C
		public void CompileBreak(ByteCode bc)
		{
			bc.Emit_Exit(this.Scope);
			this.BreakJumps.Add(bc.Emit_Jump(OpCode.Jump, -1, 0));
		}

		// Token: 0x06006704 RID: 26372 RVA: 0x00004050 File Offset: 0x00002250
		public bool IsBoundary()
		{
			return false;
		}

		// Token: 0x04005F30 RID: 24368
		public RuntimeScopeBlock Scope;

		// Token: 0x04005F31 RID: 24369
		public List<Instruction> BreakJumps = new List<Instruction>();
	}
}
