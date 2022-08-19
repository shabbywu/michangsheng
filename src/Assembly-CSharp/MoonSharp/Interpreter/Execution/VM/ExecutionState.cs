using System;
using MoonSharp.Interpreter.DataStructs;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D54 RID: 3412
	internal sealed class ExecutionState
	{
		// Token: 0x040054D9 RID: 21721
		public FastStack<DynValue> ValueStack = new FastStack<DynValue>(131072);

		// Token: 0x040054DA RID: 21722
		public FastStack<CallStackItem> ExecutionStack = new FastStack<CallStackItem>(131072);

		// Token: 0x040054DB RID: 21723
		public int InstructionPtr;

		// Token: 0x040054DC RID: 21724
		public CoroutineState State = CoroutineState.NotStarted;
	}
}
