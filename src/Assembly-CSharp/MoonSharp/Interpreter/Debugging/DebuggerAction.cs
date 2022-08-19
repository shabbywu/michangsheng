using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D64 RID: 3428
	public class DebuggerAction
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060060E0 RID: 24800 RVA: 0x002728EC File Offset: 0x00270AEC
		// (set) Token: 0x060060E1 RID: 24801 RVA: 0x002728F4 File Offset: 0x00270AF4
		public DebuggerAction.ActionType Action { get; set; }

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x002728FD File Offset: 0x00270AFD
		// (set) Token: 0x060060E3 RID: 24803 RVA: 0x00272905 File Offset: 0x00270B05
		public DateTime TimeStampUTC { get; private set; }

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x0027290E File Offset: 0x00270B0E
		// (set) Token: 0x060060E5 RID: 24805 RVA: 0x00272916 File Offset: 0x00270B16
		public int SourceID { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x0027291F File Offset: 0x00270B1F
		// (set) Token: 0x060060E7 RID: 24807 RVA: 0x00272927 File Offset: 0x00270B27
		public int SourceLine { get; set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x00272930 File Offset: 0x00270B30
		// (set) Token: 0x060060E9 RID: 24809 RVA: 0x00272938 File Offset: 0x00270B38
		public int SourceCol { get; set; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x00272941 File Offset: 0x00270B41
		// (set) Token: 0x060060EB RID: 24811 RVA: 0x00272949 File Offset: 0x00270B49
		public int[] Lines { get; set; }

		// Token: 0x060060EC RID: 24812 RVA: 0x00272952 File Offset: 0x00270B52
		public DebuggerAction()
		{
			this.TimeStampUTC = DateTime.UtcNow;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060060ED RID: 24813 RVA: 0x00272965 File Offset: 0x00270B65
		public TimeSpan Age
		{
			get
			{
				return DateTime.UtcNow - this.TimeStampUTC;
			}
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x00272978 File Offset: 0x00270B78
		public override string ToString()
		{
			if (this.Action == DebuggerAction.ActionType.ToggleBreakpoint || this.Action == DebuggerAction.ActionType.SetBreakpoint || this.Action == DebuggerAction.ActionType.ClearBreakpoint)
			{
				return string.Format("{0} {1}:({2},{3})", new object[]
				{
					this.Action,
					this.SourceID,
					this.SourceLine,
					this.SourceCol
				});
			}
			return this.Action.ToString();
		}

		// Token: 0x0200168A RID: 5770
		public enum ActionType
		{
			// Token: 0x040072EA RID: 29418
			ByteCodeStepIn,
			// Token: 0x040072EB RID: 29419
			ByteCodeStepOver,
			// Token: 0x040072EC RID: 29420
			ByteCodeStepOut,
			// Token: 0x040072ED RID: 29421
			StepIn,
			// Token: 0x040072EE RID: 29422
			StepOver,
			// Token: 0x040072EF RID: 29423
			StepOut,
			// Token: 0x040072F0 RID: 29424
			Run,
			// Token: 0x040072F1 RID: 29425
			ToggleBreakpoint,
			// Token: 0x040072F2 RID: 29426
			SetBreakpoint,
			// Token: 0x040072F3 RID: 29427
			ClearBreakpoint,
			// Token: 0x040072F4 RID: 29428
			ResetBreakpoints,
			// Token: 0x040072F5 RID: 29429
			Refresh,
			// Token: 0x040072F6 RID: 29430
			HardRefresh,
			// Token: 0x040072F7 RID: 29431
			None
		}
	}
}
