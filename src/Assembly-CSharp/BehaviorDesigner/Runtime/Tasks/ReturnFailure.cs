using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AD RID: 5293
	[TaskDescription("The return failure task will always return failure except when the child task is running.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=38")]
	[TaskIcon("{SkinColor}ReturnFailureIcon.png")]
	public class ReturnFailure : Decorator
	{
		// Token: 0x06007F09 RID: 32521 RVA: 0x0005616A File Offset: 0x0005436A
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x0005617F File Offset: 0x0005437F
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x00056188 File Offset: 0x00054388
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == 2)
			{
				return 1;
			}
			return status;
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x00056191 File Offset: 0x00054391
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04006BFA RID: 27642
		private TaskStatus executionStatus;
	}
}
