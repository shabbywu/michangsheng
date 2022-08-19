using System;
using System.Diagnostics;

namespace MoonSharp.Interpreter.Diagnostics.PerformanceCounters
{
	// Token: 0x02000D63 RID: 3427
	internal class PerformanceStopwatch : IDisposable, IPerformanceStopwatch
	{
		// Token: 0x060060DC RID: 24796 RVA: 0x00272827 File Offset: 0x00270A27
		public PerformanceStopwatch(PerformanceCounter perfcounter)
		{
			this.m_Counter = perfcounter;
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x00272841 File Offset: 0x00270A41
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

		// Token: 0x060060DE RID: 24798 RVA: 0x00272873 File Offset: 0x00270A73
		public void Dispose()
		{
			this.m_Reentrant--;
			if (this.m_Reentrant == 0)
			{
				this.m_Stopwatch.Stop();
			}
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x00272898 File Offset: 0x00270A98
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

		// Token: 0x04005554 RID: 21844
		private Stopwatch m_Stopwatch = new Stopwatch();

		// Token: 0x04005555 RID: 21845
		private int m_Count;

		// Token: 0x04005556 RID: 21846
		private int m_Reentrant;

		// Token: 0x04005557 RID: 21847
		private PerformanceCounter m_Counter;
	}
}
