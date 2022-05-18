using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02001175 RID: 4469
	internal class GlobalPerformanceStopwatch : IPerformanceStopwatch
	{
		// Token: 0x06006CC8 RID: 27848 RVA: 0x0004A2F1 File Offset: 0x000484F1
		public GlobalPerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x0004A300 File Offset: 0x00048500
		private void SignalStopwatchTerminated(Stopwatch sw)
		{
			this.m_Elapsed += sw.ElapsedMilliseconds;
			this.m_Count++;
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x0004A323 File Offset: 0x00048523
		public IDisposable Start()
		{
			return new GlobalPerformanceStopwatch.GlobalPerformanceStopwatch_StopwatchObject(this);
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x00299698 File Offset: 0x00297898
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

		// Token: 0x040061DB RID: 25051
		private int m_Count;

		// Token: 0x040061DC RID: 25052
		private long m_Elapsed;

		// Token: 0x040061DD RID: 25053
		private PerformanceCounter m_Counter;

		// Token: 0x02001176 RID: 4470
		private class GlobalPerformanceStopwatch_StopwatchObject : IDisposable
		{
			// Token: 0x06006CCC RID: 27852 RVA: 0x0004A32B File Offset: 0x0004852B
			public GlobalPerformanceStopwatch_StopwatchObject(GlobalPerformanceStopwatch parent)
			{
				this.m_Parent = parent;
				this.m_Stopwatch = Stopwatch.StartNew();
			}

			// Token: 0x06006CCD RID: 27853 RVA: 0x0004A345 File Offset: 0x00048545
			public void Dispose()
			{
				this.m_Stopwatch.Stop();
				this.m_Parent.SignalStopwatchTerminated(this.m_Stopwatch);
			}

			// Token: 0x040061DE RID: 25054
			private Stopwatch m_Stopwatch;

			// Token: 0x040061DF RID: 25055
			private GlobalPerformanceStopwatch m_Parent;
		}
	}
}
