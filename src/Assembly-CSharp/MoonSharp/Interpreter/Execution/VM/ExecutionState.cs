using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001164 RID: 4452
	internal sealed class ExecutionState
	{
		// Token: 0x04006155 RID: 24917
		public FastStack<DynValue> ValueStack = new FastStack<DynValue>(131072);

		// Token: 0x04006156 RID: 24918
		public FastStack<CallStackItem> ExecutionStack = new FastStack<CallStackItem>(131072);

		// Token: 0x04006157 RID: 24919
		public int InstructionPtr;

		// Token: 0x04006158 RID: 24920
		public CoroutineState State = CoroutineState.NotStarted;
	}
}
