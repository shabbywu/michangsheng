using System;
using System.Text;
using MoonSharp.Interpreter.Diagnostics.PerformanceCounters;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x02000D5E RID: 3422
	public class PerformanceStatistics
	{
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060060C9 RID: 24777 RVA: 0x002725FE File Offset: 0x002707FE
		// (set) Token: 0x060060CA RID: 24778 RVA: 0x00272608 File Offset: 0x00270808
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

		// Token: 0x060060CB RID: 24779 RVA: 0x00272688 File Offset: 0x00270888
		public PerformanceResult GetPerformanceCounterResult(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.GetResult();
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x002726AC File Offset: 0x002708AC
		internal IDisposable StartStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = this.m_Stopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x002726D0 File Offset: 0x002708D0
		internal static IDisposable StartGlobalStopwatch(PerformanceCounter pc)
		{
			IPerformanceStopwatch performanceStopwatch = PerformanceStatistics.m_GlobalStopwatches[(int)pc];
			if (performanceStopwatch == null)
			{
				return null;
			}
			return performanceStopwatch.Start();
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x002726F0 File Offset: 0x002708F0
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

		// Token: 0x0400554C RID: 21836
		private IPerformanceStopwatch[] m_Stopwatches = new IPerformanceStopwatch[4];

		// Token: 0x0400554D RID: 21837
		private static IPerformanceStopwatch[] m_GlobalStopwatches = new IPerformanceStopwatch[4];

		// Token: 0x0400554E RID: 21838
		private bool m_Enabled;
	}
}
