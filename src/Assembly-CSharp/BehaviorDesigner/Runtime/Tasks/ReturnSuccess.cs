using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF6 RID: 4086
	[TaskDescription("The return success task will always return success except when the child task is running.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=39")]
	[TaskIcon("{SkinColor}ReturnSuccessIcon.png")]
	public class ReturnSuccess : Decorator
	{
		// Token: 0x06007114 RID: 28948 RVA: 0x002AB202 File Offset: 0x002A9402
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x002AB217 File Offset: 0x002A9417
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x002AB220 File Offset: 0x002A9420
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == 1)
			{
				return 2;
			}
			return status;
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x002AB229 File Offset: 0x002A9429
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04005D03 RID: 23811
		private TaskStatus executionStatus;
	}
}
