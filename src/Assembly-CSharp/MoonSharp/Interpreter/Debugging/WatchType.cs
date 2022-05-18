using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02001181 RID: 4481
	public enum WatchType
	{
		// Token: 0x04006217 RID: 25111
		Watches,
		// Token: 0x04006218 RID: 25112
		VStack,
		// Token: 0x04006219 RID: 25113
		CallStack,
		// Token: 0x0400621A RID: 25114
		Coroutines,
		// Token: 0x0400621B RID: 25115
		Locals,
		// Token: 0x0400621C RID: 25116
		Threads,
		// Token: 0x0400621D RID: 25117
		MaxValue
	}
}
