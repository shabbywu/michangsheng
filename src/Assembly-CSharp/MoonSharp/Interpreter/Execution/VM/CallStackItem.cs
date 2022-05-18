using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001162 RID: 4450
	internal class CallStackItem
	{
		// Token: 0x04006142 RID: 24898
		public int Debug_EntryPoint;

		// Token: 0x04006143 RID: 24899
		public SymbolRef[] Debug_Symbols;

		// Token: 0x04006144 RID: 24900
		public SourceRef CallingSourceRef;

		// Token: 0x04006145 RID: 24901
		public CallbackFunction ClrFunction;

		// Token: 0x04006146 RID: 24902
		public CallbackFunction Continuation;

		// Token: 0x04006147 RID: 24903
		public CallbackFunction ErrorHandler;

		// Token: 0x04006148 RID: 24904
		public DynValue ErrorHandlerBeforeUnwind;

		// Token: 0x04006149 RID: 24905
		public int BasePointer;

		// Token: 0x0400614A RID: 24906
		public int ReturnAddress;

		// Token: 0x0400614B RID: 24907
		public DynValue[] LocalScope;

		// Token: 0x0400614C RID: 24908
		public ClosureContext ClosureScope;

		// Token: 0x0400614D RID: 24909
		public CallStackItemFlags Flags;
	}
}
