using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02001177 RID: 4471
	internal interface IPerformanceStopwatch
	{
		// Token: 0x06006CCE RID: 27854
		IDisposable Start();

		// Token: 0x06006CCF RID: 27855
		PerformanceResult GetResult();
	}
}
