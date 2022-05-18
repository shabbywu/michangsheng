using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AE RID: 5294
	[TaskDescription("The return success task will always return success except when the child task is running.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=39")]
	[TaskIcon("{SkinColor}ReturnSuccessIcon.png")]
	public class ReturnSuccess : Decorator
	{
		// Token: 0x06007F0E RID: 32526 RVA: 0x0005619A File Offset: 0x0005439A
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x000561AF File Offset: 0x000543AF
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x000561B8 File Offset: 0x000543B8
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == 1)
			{
				return 2;
			}
			return status;
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x000561C1 File Offset: 0x000543C1
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04006BFB RID: 27643
		private TaskStatus executionStatus;
	}
}
