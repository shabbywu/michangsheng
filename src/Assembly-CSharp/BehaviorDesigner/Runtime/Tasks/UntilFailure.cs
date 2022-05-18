using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014B0 RID: 5296
	[TaskDescription("The until failure task will keep executing its child task until the child task returns failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=41")]
	[TaskIcon("{SkinColor}UntilFailureIcon.png")]
	public class UntilFailure : Decorator
	{
		// Token: 0x06007F1A RID: 32538 RVA: 0x00056236 File Offset: 0x00054436
		public override bool CanExecute()
		{
			return this.executionStatus == 2 || this.executionStatus == 0;
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x0005624C File Offset: 0x0005444C
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x00056255 File Offset: 0x00054455
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04006C01 RID: 27649
		private TaskStatus executionStatus;
	}
}
