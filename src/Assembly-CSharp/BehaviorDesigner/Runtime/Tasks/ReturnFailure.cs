using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF5 RID: 4085
	[TaskDescription("The return failure task will always return failure except when the child task is running.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=38")]
	[TaskIcon("{SkinColor}ReturnFailureIcon.png")]
	public class ReturnFailure : Decorator
	{
		// Token: 0x0600710F RID: 28943 RVA: 0x002AB1D2 File Offset: 0x002A93D2
		public override bool CanExecute()
		{
			return this.executionStatus == null || this.executionStatus == 3;
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x002AB1E7 File Offset: 0x002A93E7
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x002AB1F0 File Offset: 0x002A93F0
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == 2)
			{
				return 1;
			}
			return status;
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x002AB1F9 File Offset: 0x002A93F9
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04005D02 RID: 23810
		private TaskStatus executionStatus;
	}
}
