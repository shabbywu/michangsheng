using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF2 RID: 4082
	[TaskDescription("The interrupt task will stop all child tasks from running if it is interrupted. The interruption can be triggered by the perform interruption task. The interrupt task will keep running its child until this interruption is called. If no interruption happens and the child task completed its execution the interrupt task will return the value assigned by the child task.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=35")]
	[TaskIcon("{SkinColor}InterruptIcon.png")]
	public class Interrupt : Decorator
	{
		// Token: 0x060070FF RID: 28927 RVA: 0x002AB087 File Offset: 0x002A9287
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x002AB09C File Offset: 0x002A929C
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x002AB0A5 File Offset: 0x002A92A5
		public void DoInterrupt(TaskStatus status)
		{
			this.interruptStatus = status;
			BehaviorManager.instance.Interrupt(base.Owner, this);
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x002AB0BF File Offset: 0x002A92BF
		public override TaskStatus OverrideStatus()
		{
			return this.interruptStatus;
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x002AB0C7 File Offset: 0x002A92C7
		public override void OnEnd()
		{
			this.interruptStatus = 1;
			this.executionStatus = 0;
		}

		// Token: 0x04005CFA RID: 23802
		private TaskStatus interruptStatus = 1;

		// Token: 0x04005CFB RID: 23803
		private TaskStatus executionStatus;
	}
}
