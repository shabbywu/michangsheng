using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF9 RID: 4089
	[TaskDescription("The until success task will keep executing its child task until the child task returns success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=42")]
	[TaskIcon("{SkinColor}UntilSuccessIcon.png")]
	public class UntilSuccess : Decorator
	{
		// Token: 0x06007124 RID: 28964 RVA: 0x002AB357 File Offset: 0x002A9557
		public override bool CanExecute()
		{
			return this.executionStatus == 1 || this.executionStatus == 0;
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x002AB36D File Offset: 0x002A956D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x002AB376 File Offset: 0x002A9576
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04005D0A RID: 23818
		private TaskStatus executionStatus;
	}
}
