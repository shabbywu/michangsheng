using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014B1 RID: 5297
	[TaskDescription("The until success task will keep executing its child task until the child task returns success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=42")]
	[TaskIcon("{SkinColor}UntilSuccessIcon.png")]
	public class UntilSuccess : Decorator
	{
		// Token: 0x06007F1E RID: 32542 RVA: 0x0005625E File Offset: 0x0005445E
		public override bool CanExecute()
		{
			return this.executionStatus == 1 || this.executionStatus == 0;
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x00056274 File Offset: 0x00054474
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x0005627D File Offset: 0x0005447D
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04006C02 RID: 27650
		private TaskStatus executionStatus;
	}
}
