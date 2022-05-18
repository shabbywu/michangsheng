using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02001178 RID: 4472
	internal class PerformanceStopwatch : IDisposable, IPerformanceStopwatch
	{
		// Token: 0x06006CD0 RID: 27856 RVA: 0x0004A363 File Offset: 0x00048563
		public PerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x0004A37D File Offset: 0x0004857D
		public IDisposable Start()
		{
			if (this.m_Reentrant == 0)
			{
				this.m_Count++;
				this.m_Stopwatch.Start();
			}
			this.m_Reentrant++;
			return this;
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x0004A3AF File Offset: 0x000485AF
		public void Dispose()
		{
			this.m_Reentrant--;
			if (this.m_Reentrant == 0)
			{
				this.m_Stopwatch.Stop();
			}
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x002996E8 File Offset: 0x002978E8
		public PerformanceResult GetResult()
		{
			return new PerformanceResult
			{
				Type = PerformanceCounterType.TimeMilliseconds,
				Global = false,
				Name = this.m_Counter.ToString(),
				Instances = this.m_Count,
				Counter = this.m_Stopwatch.ElapsedMilliseconds
			};
		}

		// Token: 0x040061E0 RID: 25056
		private Stopwatch m_Stopwatch = new Stopwatch();

		// Token: 0x040061E1 RID: 25057
		private int m_Count;

		// Token: 0x040061E2 RID: 25058
		private int m_Reentrant;

		// Token: 0x040061E3 RID: 25059
		private PerformanceCounter m_Counter;
	}
}
