using System;
using System.Text;
using MoonSharp.Interpreter.Diagnostics.PerformanceCounters;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x02001172 RID: 4466
	public class PerformanceStatistics
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06006CBB RID: 27835 RVA: 0x0004A279 File Offset: 0x00048479
		// (set) Token: 0x06006CBC RID: 27836 RVA: 0x00299570 File Offset: 0x00297770
		public bool Enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				if (value && !this.m_Enabled)
				{
					if (PerformanceStatistics.m_GlobalStopwatches[3] == null)
					{
						PerformanceStatistics.m_GlobalStopwatches[3] = new GlobalPerformanceStopwatch(PerformanceCounter.AdaptersCompilation);
					}
					for (int i = 0; i < 4; i++)
					{
						this.m_Stopwatches[i] = (PerformanceStatistics.m_GlobalStopwatches[i] ?? new PerformanceStopwatch((PerformanceCounter)i));
					}
				}
				else if (!value && this.m_Enabled)
				{
					this.m_Stopwatches = new IPerformanceStopwatch[4];
					PerformanceStatistics.m_GlobalStopwatches = new IPerformanceStopwatch[4];
				}
				this.m_Enabled = value;
			}
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x002995F0 File Offset: 0x002977F0
		public PerformanceResult GetPerformanceCounterResult(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.GetResult();
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00299614 File Offset: 0x00297814
		internal IDisposable StartStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x00299638 File Offset: 0x00297838
		internal static IDisposable StartGlobalStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = PerformanceStatistics.m_GlobalStopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x00299658 File Offset: 0x00297858
		public string GetPerformanceLog()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				PerformanceResult performanceCounterResult = this.GetPerformanceCounterResult((PerformanceCounter)i);
				if (performanceCounterResult != null)
				{
					stringBuilder.AppendLine(performanceCounterResult.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040061D6 RID: 25046
		private IPerformanceStopwatch[] m_Stopwatches = new IPerformanceStopwatch[4];

		// Token: 0x040061D7 RID: 25047
		private static IPerformanceStopwatch[] m_GlobalStopwatches = new IPerformanceStopwatch[4];

		// Token: 0x040061D8 RID: 25048
		private bool m_Enabled;
	}
}
