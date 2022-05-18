using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AA RID: 5290
	[TaskDescription("The interrupt task will stop all child tasks from running if it is interrupted. The interruption can be triggered by the perform interruption task. The interrupt task will keep running its child until this interruption is called. If no interruption happens and the child task completed its execution the interrupt task will return the value assigned by the child task.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=35")]
	[TaskIcon("{SkinColor}InterruptIcon.png")]
	public class Interrupt : Decorator
	{
		// Token: 0x06007EF9 RID: 32505 RVA: 0x00056078 File Offset: 0x00054278
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x0005608D File Offset: 0x0005428D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x00056096 File Offset: 0x00054296
		public void DoInterrupt(TaskStatus status)
		{
			this.interruptStatus = status;
			BehaviorManager.instance.Interrupt(base.Owner, this);
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x000560B0 File Offset: 0x000542B0
		public override TaskStatus OverrideStatus()
		{
			return this.interruptStatus;
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x000560B8 File Offset: 0x000542B8
		public override void OnEnd()
		{
			this.interruptStatus = 1;
			this.executionStatus = 0;
		}

		// Token: 0x04006BF2 RID: 27634
		private TaskStatus interruptStatus = 1;

		// Token: 0x04006BF3 RID: 27635
		private TaskStatus executionStatus;
	}
}
