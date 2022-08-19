using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02000D53 RID: 3411
	[Flags]
	internal enum CallStackItemFlags
	{
		// Token: 0x040054D3 RID: 21715
		None = 0,
		// Token: 0x040054D4 RID: 21716
		EntryPoint = 1,
		// Token: 0x040054D5 RID: 21717
		ResumeEntryPoint = 3,
		// Token: 0x040054D6 RID: 21718
		CallEntryPoint = 5,
		// Token: 0x040054D7 RID: 21719
		TailCall = 16,
		// Token: 0x040054D8 RID: 21720
		MethodCall = 32
	}
}
