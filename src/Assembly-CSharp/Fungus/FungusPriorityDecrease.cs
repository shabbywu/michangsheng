using System;

namespace Fungus
{
	// Token: 0x02000E13 RID: 3603
	[CommandInfo("Priority Signals", "Priority Down", "Decrease the FungusPriority count, causing the related FungusPrioritySignals to fire. Intended to be used to notify external systems that fungus is doing something important and they should perhaps resume.", 0)]
	public class FungusPriorityDecrease : Command
	{
		// Token: 0x060065C1 RID: 26049 RVA: 0x00283FBC File Offset: 0x002821BC
		public override void OnEnter()
		{
			FungusPrioritySignals.DoDecreasePriorityDepth();
			this.Continue();
		}
	}
}
