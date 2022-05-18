using System;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02001179 RID: 4473
	public class DebuggerAction
	{
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x0004A3D2 File Offset: 0x000485D2
		// (set) Token: 0x06006CD5 RID: 27861 RVA: 0x0004A3DA File Offset: 0x000485DA
		public DebuggerAction.ActionType Action { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x0004A3E3 File Offset: 0x000485E3
		// (set) Token: 0x06006CD7 RID: 27863 RVA: 0x0004A3EB File Offset: 0x000485EB
		public DateTime TimeStampUTC { get; private set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06006CD8 RID: 27864 RVA: 0x0004A3F4 File Offset: 0x000485F4
		// (set) Token: 0x06006CD9 RID: 27865 RVA: 0x0004A3FC File Offset: 0x000485FC
		public int SourceID { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06006CDA RID: 27866 RVA: 0x0004A405 File Offset: 0x00048605
		// (set) Token: 0x06006CDB RID: 27867 RVA: 0x0004A40D File Offset: 0x0004860D
		public int SourceLine { get; set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06006CDC RID: 27868 RVA: 0x0004A416 File Offset: 0x00048616
		// (set) Token: 0x06006CDD RID: 27869 RVA: 0x0004A41E File Offset: 0x0004861E
		public int SourceCol { get; set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06006CDE RID: 27870 RVA: 0x0004A427 File Offset: 0x00048627
		// (set) Token: 0x06006CDF RID: 27871 RVA: 0x0004A42F File Offset: 0x0004862F
		public int[] Lines { get; set; }

		// Token: 0x06006CE0 RID: 27872 RVA: 0x0004A438 File Offset: 0x00048638
		public DebuggerAction()
		{
			this.TimeStampUTC = DateTime.UtcNow;
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x0004A44B File Offset: 0x0004864B
		public TimeSpan Age
		{
			get
			{
				return DateTime.UtcNow - this.TimeStampUTC;
			}
		}

		// Token: 0x06006CE2 RID: 27874 RVA: 0x0029973C File Offset: 0x0029793C
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

		// Token: 0x0200117A RID: 4474
		public enum ActionType
		{
			// Token: 0x040061EB RID: 25067
			ByteCodeStepIn,
			// Token: 0x040061EC RID: 25068
			ByteCodeStepOver,
			// Token: 0x040061ED RID: 25069
			ByteCodeStepOut,
			// Token: 0x040061EE RID: 25070
			StepIn,
			// Token: 0x040061EF RID: 25071
			StepOver,
			// Token: 0x040061F0 RID: 25072
			StepOut,
			// Token: 0x040061F1 RID: 25073
			Run,
			// Token: 0x040061F2 RID: 25074
			ToggleBreakpoint,
			// Token: 0x040061F3 RID: 25075
			SetBreakpoint,
			// Token: 0x040061F4 RID: 25076
			ClearBreakpoint,
			// Token: 0x040061F5 RID: 25077
			ResetBreakpoints,
			// Token: 0x040061F6 RID: 25078
			Refresh,
			// Token: 0x040061F7 RID: 25079
			HardRefresh,
			// Token: 0x040061F8 RID: 25080
			None
		}
	}
}
