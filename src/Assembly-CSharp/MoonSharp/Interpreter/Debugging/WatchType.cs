using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D6B RID: 3435
	public enum WatchType
	{
		// Token: 0x0400557C RID: 21884
		Watches,
		// Token: 0x0400557D RID: 21885
		VStack,
		// Token: 0x0400557E RID: 21886
		CallStack,
		// Token: 0x0400557F RID: 21887
		Coroutines,
		// Token: 0x04005580 RID: 21888
		Locals,
		// Token: 0x04005581 RID: 21889
		Threads,
		// Token: 0x04005582 RID: 21890
		MaxValue
	}
}
