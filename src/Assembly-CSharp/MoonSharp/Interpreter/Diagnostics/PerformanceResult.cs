using System;

namespace MoonSharp.Interpreter.Diagnostics
{
	// Token: 0x02000D5D RID: 3421
	public class PerformanceResult
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x0027250F File Offset: 0x0027070F
		// (set) Token: 0x060060BD RID: 24765 RVA: 0x00272517 File Offset: 0x00270717
		public string Name { get; internal set; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x00272520 File Offset: 0x00270720
		// (set) Token: 0x060060BF RID: 24767 RVA: 0x00272528 File Offset: 0x00270728
		public long Counter { get; internal set; }

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x00272531 File Offset: 0x00270731
		// (set) Token: 0x060060C1 RID: 24769 RVA: 0x00272539 File Offset: 0x00270739
		public int Instances { get; internal set; }

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00272542 File Offset: 0x00270742
		// (set) Token: 0x060060C3 RID: 24771 RVA: 0x0027254A File Offset: 0x0027074A
		public bool Global { get; internal set; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x00272553 File Offset: 0x00270753
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x0027255B File Offset: 0x0027075B
		public PerformanceCounterType Type { get; internal set; }

		// Token: 0x060060C6 RID: 24774 RVA: 0x00272564 File Offset: 0x00270764
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

		// Token: 0x060060C7 RID: 24775 RVA: 0x002725CB File Offset: 0x002707CB
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
