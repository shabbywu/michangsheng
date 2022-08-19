using System;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02000D60 RID: 3424
	internal class DummyPerformanceStopwatch : IPerformanceStopwatch, IDisposable
	{
		// Token: 0x060060D1 RID: 24785 RVA: 0x0027274E File Offset: 0x0027094E
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

		// Token: 0x060060D2 RID: 24786 RVA: 0x00183D24 File Offset: 0x00181F24
		public IDisposable Start()
		{
			return this;
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x00272789 File Offset: 0x00270989
		public PerformanceResult GetResult()
		{
			return this.m_Result;
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x00004095 File Offset: 0x00002295
		public void Dispose()
		{
		}

		// Token: 0x0400554F RID: 21839
		public static DummyPerformanceStopwatch Instance = new DummyPerformanceStopwatch();

		// Token: 0x04005550 RID: 21840
		private PerformanceResult m_Result;
	}
}
