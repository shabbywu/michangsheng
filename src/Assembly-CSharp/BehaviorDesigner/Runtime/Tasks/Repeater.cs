using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF4 RID: 4084
	[TaskDescription("The repeater task will repeat execution of its child task until the child task has been run a specified number of times. It has the option of continuing to execute the child task even if the child task returns a failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=37")]
	[TaskIcon("{SkinColor}RepeaterIcon.png")]
	public class Repeater : Decorator
	{
		// Token: 0x0600710A RID: 28938 RVA: 0x002AB124 File Offset: 0x002A9324
		public override bool CanExecute()
		{
			return (this.repeatForever.Value || this.executionCount < this.count.Value) && (!this.endOnFailure.Value || (this.endOnFailure.Value && this.executionStatus != 1));
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x002AB17D File Offset: 0x002A937D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionCount++;
			this.executionStatus = childStatus;
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x002AB194 File Offset: 0x002A9394
		public override void OnEnd()
		{
			this.executionCount = 0;
			this.executionStatus = 0;
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x002AB1A4 File Offset: 0x002A93A4
		public override void OnReset()
		{
			this.count = 0;
			this.endOnFailure = true;
		}

		// Token: 0x04005CFD RID: 23805
		[Tooltip("The number of times to repeat the execution of its child task")]
		public SharedInt count = 1;

		// Token: 0x04005CFE RID: 23806
		[Tooltip("Allows the repeater to repeat forever")]
		public SharedBool repeatForever;

		// Token: 0x04005CFF RID: 23807
		[Tooltip("Should the task return if the child task returns a failure")]
		public SharedBool endOnFailure;

		// Token: 0x04005D00 RID: 23808
		private int executionCount;

		// Token: 0x04005D01 RID: 23809
		private TaskStatus executionStatus;
	}
}
