using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02001180 RID: 4480
	public class WatchItem
	{
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06006D19 RID: 27929 RVA: 0x0004A5E8 File Offset: 0x000487E8
		// (set) Token: 0x06006D1A RID: 27930 RVA: 0x0004A5F0 File Offset: 0x000487F0
		public int Address { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06006D1B RID: 27931 RVA: 0x0004A5F9 File Offset: 0x000487F9
		// (set) Token: 0x06006D1C RID: 27932 RVA: 0x0004A601 File Offset: 0x00048801
		public int BasePtr { get; set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06006D1D RID: 27933 RVA: 0x0004A60A File Offset: 0x0004880A
		// (set) Token: 0x06006D1E RID: 27934 RVA: 0x0004A612 File Offset: 0x00048812
		public int RetAddress { get; set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06006D1F RID: 27935 RVA: 0x0004A61B File Offset: 0x0004881B
		// (set) Token: 0x06006D20 RID: 27936 RVA: 0x0004A623 File Offset: 0x00048823
		public string Name { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06006D21 RID: 27937 RVA: 0x0004A62C File Offset: 0x0004882C
		// (set) Token: 0x06006D22 RID: 27938 RVA: 0x0004A634 File Offset: 0x00048834
		public DynValue Value { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06006D23 RID: 27939 RVA: 0x0004A63D File Offset: 0x0004883D
		// (set) Token: 0x06006D24 RID: 27940 RVA: 0x0004A645 File Offset: 0x00048845
		public SymbolRef LValue { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06006D25 RID: 27941 RVA: 0x0004A64E File Offset: 0x0004884E
		// (set) Token: 0x06006D26 RID: 27942 RVA: 0x0004A656 File Offset: 0x00048856
		public bool IsError { get; set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x0004A65F File Offset: 0x0004885F
		// (set) Token: 0x06006D28 RID: 27944 RVA: 0x0004A667 File Offset: 0x00048867
		public SourceRef Location { get; set; }

		// Token: 0x06006D29 RID: 27945 RVA: 0x00299CF8 File Offset: 0x00297EF8
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}", new object[]
			{
				this.Address,
				this.BasePtr,
				this.RetAddress,
				this.Name ?? "(null)",
				(this.Value != null) ? this.Value.ToString() : "(null)",
				(this.LValue != null) ? this.LValue.ToString() : "(null)"
			});
		}
	}
}
