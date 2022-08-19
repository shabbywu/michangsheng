using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02000D62 RID: 3426
	internal interface IPerformanceStopwatch
	{
		// Token: 0x060060DA RID: 24794
		IDisposable Start();

		// Token: 0x060060DB RID: 24795
		PerformanceResult GetResult();
	}
}
