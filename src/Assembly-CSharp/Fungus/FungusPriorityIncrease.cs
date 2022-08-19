using System;

namespace Fungus
{
	// Token: 0x02000E14 RID: 3604
	[CommandInfo("Priority Signals", "Priority Up", "Increases the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps pause.", 0)]
	public class FungusPriorityIncrease : Command
	{
		// Token: 0x060065C3 RID: 26051 RVA: 0x00283FC9 File Offset: 0x002821C9
		public override void OnEnter()
		{
			FungusPrioritySignals.DoIncreasePriorityDepth();
			this.Continue();
		}
	}
}
