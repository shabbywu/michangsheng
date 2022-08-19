using System;
using MoonSharp.Interpreter.Debugging;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D52 RID: 3410
	internal class CallStackItem
	{
		// Token: 0x040054C6 RID: 21702
		public int Debug_EntryPoint;

		// Token: 0x040054C7 RID: 21703
		public SymbolRef[] Debug_Symbols;

		// Token: 0x040054C8 RID: 21704
		public SourceRef CallingSourceRef;

		// Token: 0x040054C9 RID: 21705
		public CallbackFunction ClrFunction;

		// Token: 0x040054CA RID: 21706
		public CallbackFunction Continuation;

		// Token: 0x040054CB RID: 21707
		public CallbackFunction ErrorHandler;

		// Token: 0x040054CC RID: 21708
		public DynValue ErrorHandlerBeforeUnwind;

		// Token: 0x040054CD RID: 21709
		public int BasePointer;

		// Token: 0x040054CE RID: 21710
		public int ReturnAddress;

		// Token: 0x040054CF RID: 21711
		public DynValue[] LocalScope;

		// Token: 0x040054D0 RID: 21712
		public ClosureContext ClosureScope;

		// Token: 0x040054D1 RID: 21713
		public CallStackItemFlags Flags;
	}
}
