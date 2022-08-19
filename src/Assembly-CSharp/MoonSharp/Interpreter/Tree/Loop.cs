using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x02000CCC RID: 3276
	internal class Loop : ILoop
	{
		// Token: 0x06005BFA RID: 23546 RVA: 0x0025D10D File Offset: 0x0025B30D
		public void CompileBreak(ByteCode bc)
		{
			bc.Emit_Exit(this.Scope);
			this.BreakJumps.Add(bc.Emit_Jump(OpCode.Jump, -1, 0));
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool IsBoundary()
		{
			return false;
		}

		// Token: 0x0400534D RID: 21325
		public RuntimeScopeBlock Scope;

		// Token: 0x0400534E RID: 21326
		public List<Instruction> BreakJumps = new List<Instruction>();
	}
}
