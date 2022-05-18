using System;

namespace Fungus
{
	// Token: 0x02001260 RID: 4704
	[CommandInfo("Priority Signals", "Priority Down", "Decrease the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps resume.", 0)]
	public class FungusPriorityDecrease : Command
	{
		// Token: 0x0600724F RID: 29263 RVA: 0x0004DCD0 File Offset: 0x0004BED0
		public override void OnEnter()
		{
			FungusPrioritySignals.DoDecreasePriorityDepth();
			this.Continue();
		}
	}
}
