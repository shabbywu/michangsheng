using System;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x02001171 RID: 4465
	public class PerformanceResult
	{
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x0004A1F1 File Offset: 0x000483F1
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x0004A1F9 File Offset: 0x000483F9
		public string Name { get; internal set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x0004A202 File Offset: 0x00048402
		// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x0004A20A File Offset: 0x0004840A
		public long Counter { get; internal set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x0004A213 File Offset: 0x00048413
		// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x0004A21B File Offset: 0x0004841B
		public int Instances { get; internal set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x0004A224 File Offset: 0x00048424
		// (set) Token: 0x06006CB5 RID: 27829 RVA: 0x0004A22C File Offset: 0x0004842C
		public bool Global { get; internal set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x0004A235 File Offset: 0x00048435
		// (set) Token: 0x06006CB7 RID: 27831 RVA: 0x0004A23D File Offset: 0x0004843D
		public PerformanceCounterType Type { get; internal set; }

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00299508 File Offset: 0x00297708
		public override string ToString()
		{
			return string.Format("{0}{1} : {2} times / {3} {4}", new object[]
			{
				this.Name,
				this.Global ? "(g)" : "",
				this.Instances,
				this.Counter,
				PerformanceResult.PerformanceCounterTypeToString(this.Type)
			});
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x0004A246 File Offset: 0x00048446
		public static string PerformanceCounterTypeToString(PerformanceCounterType Type)
		{
			if (Type == PerformanceCounterType.MemoryBytes)
			{
				return "bytes";
			}
			if (Type != PerformanceCounterType.TimeMilliseconds)
			{
				throw new InvalidOperationException("PerformanceCounterType has invalid value " + Type.ToString());
			}
			return "ms";
		}
	}
}
