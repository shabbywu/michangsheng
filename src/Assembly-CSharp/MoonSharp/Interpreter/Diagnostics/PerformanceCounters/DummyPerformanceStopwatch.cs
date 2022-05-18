using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02001174 RID: 4468
	internal class DummyPerformanceStopwatch : IPerformanceStopwatch, IDisposable
	{
		// Token: 0x06006CC3 RID: 27843 RVA: 0x0004A2A2 File Offset: 0x000484A2
		private DummyPerformanceStopwatch()
		{
			this.m_Result = new PerformanceResult
			{
				Counter = 0L,
				Global = true,
				Instances = 0,
				Name = "::dummy::",
				Type = PerformanceCounterType.TimeMilliseconds
			};
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x0002FB09 File Offset: 0x0002DD09
		public IDisposable Start()
		{
			return this;
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x0004A2DD File Offset: 0x000484DD
		public PerformanceResult GetResult()
		{
			return this.m_Result;
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x000042DD File Offset: 0x000024DD
		public void Dispose()
		{
		}

		// Token: 0x040061D9 RID: 25049
		public static DummyPerformanceStopwatch Instance = new DummyPerformanceStopwatch();

		// Token: 0x040061DA RID: 25050
		private PerformanceResult m_Result;
	}
}
