using System;

namespace Fungus
{
	// Token: 0x02001262 RID: 4706
	[CommandInfo("Priority Signals", "Priority Reset", "Resets the FungusPriority count to zero. Useful if you are among logic that is hard to have matching increase and decreases.", 0)]
	public class FungusPriorityReset : Command
	{
		// Token: 0x06007253 RID: 29267 RVA: 0x0004DCEA File Offset: 0x0004BEEA
		public override void OnEnter()
		{
			FungusPrioritySignals.DoResetPriority();
			this.Continue();
		}
	}
}
