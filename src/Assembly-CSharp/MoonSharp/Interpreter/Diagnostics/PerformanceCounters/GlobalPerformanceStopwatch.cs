using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02000D61 RID: 3425
	internal class GlobalPerformanceStopwatch : IPerformanceStopwatch
	{
		// Token: 0x060060D6 RID: 24790 RVA: 0x0027279D File Offset: 0x0027099D
		public GlobalPerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x002727AC File Offset: 0x002709AC
		private void SignalStopwatchTerminated(Stopwatch sw)
		{
			this.m_Elapsed += sw.ElapsedMilliseconds;
			this.m_Count++;
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x002727CF File Offset: 0x002709CF
		public IDisposable Start()
		{
			return new GlobalPerformanceStopwatch.GlobalPerformanceStopwatch_StopwatchObject(this);
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x002727D8 File Offset: 0x002709D8
		public PerformanceResult GetResult()
		{
			return new PerformanceResult
			{
				Type = PerformanceCounterType.TimeMilliseconds,
				Global = false,
				Name = this.m_Counter.ToString(),
				Instances = this.m_Count,
				Counter = this.m_Elapsed
			};
		}

		// Token: 0x04005551 RID: 21841
		private int m_Count;

		// Token: 0x04005552 RID: 21842
		private long m_Elapsed;

		// Token: 0x04005553 RID: 21843
		private PerformanceCounter m_Counter;

		// Token: 0x02001689 RID: 5769
		private class GlobalPerformanceStopwatch_StopwatchObject : IDisposable
		{
			// Token: 0x0600875D RID: 34653 RVA: 0x002E6F0E File Offset: 0x002E510E
			public GlobalPerformanceStopwatch_StopwatchObject(GlobalPerformanceStopwatch parent)
			{
				this.m_Parent = parent;
				this.m_Stopwatch = Stopwatch.StartNew();
			}

			// Token: 0x0600875E RID: 34654 RVA: 0x002E6F28 File Offset: 0x002E5128
			public void Dispose()
			{
				this.m_Stopwatch.Stop();
				this.m_Parent.SignalStopwatchTerminated(this.m_Stopwatch);
			}

			// Token: 0x040072E7 RID: 29415
			private Stopwatch m_Stopwatch;

			// Token: 0x040072E8 RID: 29416
			private GlobalPerformanceStopwatch m_Parent;
		}
	}
}
