using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x0200117B RID: 4475
	[Flags]
	public enum DebuggerCaps
	{
		// Token: 0x040061FA RID: 25082
		CanDebugSourceCode = 1,
		// Token: 0x040061FB RID: 25083
		CanDebugByteCode = 2,
		// Token: 0x040061FC RID: 25084
		HasLineBasedBreakpoints = 4
	}
}
