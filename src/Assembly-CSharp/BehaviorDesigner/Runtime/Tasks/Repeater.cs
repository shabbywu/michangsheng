using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AC RID: 5292
	[TaskDescription("The repeater task will repeat execution of its child task until the child task has been run a specified number of times. It has the option of continuing to execute the child task even if the child task returns a failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=37")]
	[TaskIcon("{SkinColor}RepeaterIcon.png")]
	public class Repeater : Decorator
	{
		// Token: 0x06007F04 RID: 32516 RVA: 0x002C99A8 File Offset: 0x002C7BA8
		public override bool CanExecute()
		{
			return (this.repeatForever.Value || this.executionCount < this.count.Value) && (!this.endOnFailure.Value || (this.endOnFailure.Value && this.executionStatus != 1));
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x00056115 File Offset: 0x00054315
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionCount++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x0005612C File Offset: 0x0005432C
		public override void OnEnd()
		{
			this.executionCount = 0;
			this.executionStatus = 0;
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x0005613C File Offset: 0x0005433C
		public override void OnReset()
		{
			this.count = 0;
			this.endOnFailure = true;
		}

		// Token: 0x04006BF5 RID: 27637
		[Tooltip("The number of times to repeat the execution of its child task")]
		public SharedInt count = 1;

		// Token: 0x04006BF6 RID: 27638
		[Tooltip("Allows the repeater to repeat forever")]
		public SharedBool repeatForever;

		// Token: 0x04006BF7 RID: 27639
		[Tooltip("Should the task return if the child task returns a failure")]
		public SharedBool endOnFailure;

		// Token: 0x04006BF8 RID: 27640
		private int executionCount;

		// Token: 0x04006BF9 RID: 27641
		private TaskStatus executionStatus;
	}
}
