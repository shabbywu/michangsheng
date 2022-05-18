using System;

namespace Fungus
{
	// Token: 0x02001261 RID: 4705
	[CommandInfo("Priority Signals", "Priority Up", "Increases the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps pause.", 0)]
	public class FungusPriorityIncrease : Command
	{
		// Token: 0x06007251 RID: 29265 RVA: 0x0004DCDD File Offset: 0x0004BEDD
		public override void OnEnter()
		{
			FungusPrioritySignals.DoIncreasePriorityDepth();
			this.Continue();
		}
	}
}
