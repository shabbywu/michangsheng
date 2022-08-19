using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FD0 RID: 4048
	[TaskDescription("Perform the actual interruption. This will immediately stop the specified tasks from running and will return success or failure depending on the value of interrupt success.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=17")]
	[TaskIcon("{SkinColor}PerformInterruptionIcon.png")]
	public class PerformInterruption : Action
	{
		// Token: 0x06007031 RID: 28721 RVA: 0x002A8E74 File Offset: 0x002A7074
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.interruptTasks.Length; i++)
			{
				this.interruptTasks[i].DoInterrupt(this.interruptSuccess.Value ? 2 : 1);
			}
			return 2;
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x002A8EB3 File Offset: 0x002A70B3
		public override void OnReset()
		{
			this.interruptTasks = null;
			this.interruptSuccess = false;
		}

		// Token: 0x04005C76 RID: 23670
		[Tooltip("The list of tasks to interrupt. Can be any number of tasks")]
		public Interrupt[] interruptTasks;

		// Token: 0x04005C77 RID: 23671
		[Tooltip("When we interrupt the task should we return a task status of success?")]
		public SharedBool interruptSuccess;
	}
}
