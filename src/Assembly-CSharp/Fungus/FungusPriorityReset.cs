using System;

namespace Fungus
{
	// Token: 0x02000E15 RID: 3605
	[CommandInfo("Priority Signals", "Priority Reset", "Resets the FungusPriority count to zero. Useful if you are among logic that is hard to have matching increase and decreases.", 0)]
	public class FungusPriorityReset : Command
	{
		// Token: 0x060065C5 RID: 26053 RVA: 0x00283FD6 File Offset: 0x002821D6
		public override void OnEnter()
		{
			FungusPrioritySignals.DoResetPriority();
			this.Continue();
		}
	}
}
