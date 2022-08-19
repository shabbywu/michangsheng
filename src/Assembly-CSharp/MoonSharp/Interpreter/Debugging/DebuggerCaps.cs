using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D65 RID: 3429
	[Flags]
	public enum DebuggerCaps
	{
		// Token: 0x0400555F RID: 21855
		CanDebugSourceCode = 1,
		// Token: 0x04005560 RID: 21856
		CanDebugByteCode = 2,
		// Token: 0x04005561 RID: 21857
		HasLineBasedBreakpoints = 4
	}
}
