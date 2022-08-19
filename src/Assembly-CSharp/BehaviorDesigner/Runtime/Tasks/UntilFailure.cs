using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF8 RID: 4088
	[TaskDescription("The until failure task will keep executing its child task until the child task returns failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=41")]
	[TaskIcon("{SkinColor}UntilFailureIcon.png")]
	public class UntilFailure : Decorator
	{
		// Token: 0x06007120 RID: 28960 RVA: 0x002AB32F File Offset: 0x002A952F
		public override bool CanExecute()
		{
			return this.executionStatus == 2 || this.executionStatus == 0;
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x002AB345 File Offset: 0x002A9545
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x002AB34E File Offset: 0x002A954E
		public override void OnEnd()
		{
			this.executionStatus = 0;
		}

		// Token: 0x04005D09 RID: 23817
		private TaskStatus executionStatus;
	}
}
