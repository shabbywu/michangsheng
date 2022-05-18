using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001488 RID: 5256
	[TaskDescription("Perform the actual interruption. This will immediately stop the specified tasks from running and will return success or failure depending on the value of interrupt success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=17")]
	[TaskIcon("{SkinColor}PerformInterruptionIcon.png")]
	public class PerformInterruption : Action
	{
		// Token: 0x06007E2B RID: 32299 RVA: 0x002C8328 File Offset: 0x002C6528
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.interruptTasks.Length; i++)
			{
				this.interruptTasks[i].DoInterrupt(this.interruptSuccess.Value ? 2 : 1);
			}
			return 2;
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x000554F2 File Offset: 0x000536F2
		public override void OnReset()
		{
			this.interruptTasks = null;
			this.interruptSuccess = false;
		}

		// Token: 0x04006B6E RID: 27502
		[Tooltip("The list of tasks to interrupt. Can be any number of tasks")]
		public Interrupt[] interruptTasks;

		// Token: 0x04006B6F RID: 27503
		[Tooltip("When we interrupt the task should we return a task status of success?")]
		public SharedBool interruptSuccess;
	}
}
