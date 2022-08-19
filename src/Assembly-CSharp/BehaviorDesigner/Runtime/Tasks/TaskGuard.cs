using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02000FF7 RID: 4087
	[TaskDescription("The task guard task is similar to a semaphore in multithreaded programming. The task guard task is there to ensure a limited resource is not being overused. \n\nFor example, you may place a task guard above a task that plays an animation. Elsewhere within your behavior tree you may also have another task that plays a different animation but uses the same bones for that animation. Because of this you don't want that animation to play twice at the same time. Placing a task guard will let you specify how many times a particular task can be accessed at the same time.\n\nIn the previous animation task example you would specify an access count of 1. With this setup the animation task can be only controlled by one task at a time. If the first task is playing the animation and a second task wants to control the animation as well, it will either have to wait or skip over the task completely.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=40")]
	[TaskIcon("{SkinColor}TaskGuardIcon.png")]
	public class TaskGuard : Decorator
	{
		// Token: 0x06007119 RID: 28953 RVA: 0x002AB232 File Offset: 0x002A9432
		public override bool CanExecute()
		{
			return this.executingTasks < this.maxTaskAccessCount.Value && !this.executing;
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x002AB254 File Offset: 0x002A9454
		public override void OnChildStarted()
		{
			this.executingTasks++;
			this.executing = true;
			for (int i = 0; i < this.linkedTaskGuards.Length; i++)
			{
				this.linkedTaskGuards[i].taskExecuting(true);
			}
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x002AB297 File Offset: 0x002A9497
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.executing || !this.waitUntilTaskAvailable.Value)
			{
				return status;
			}
			return 3;
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x002AB2B1 File Offset: 0x002A94B1
		public void taskExecuting(bool increase)
		{
			this.executingTasks += (increase ? 1 : -1);
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x002AB2C8 File Offset: 0x002A94C8
		public override void OnEnd()
		{
			if (this.executing)
			{
				this.executingTasks--;
				for (int i = 0; i < this.linkedTaskGuards.Length; i++)
				{
					this.linkedTaskGuards[i].taskExecuting(false);
				}
				this.executing = false;
			}
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x002AB313 File Offset: 0x002A9513
		public override void OnReset()
		{
			this.maxTaskAccessCount = null;
			this.linkedTaskGuards = null;
			this.waitUntilTaskAvailable = true;
		}

		// Token: 0x04005D04 RID: 23812
		[Tooltip("The number of times the child tasks can be accessed by parallel tasks at once")]
		public SharedInt maxTaskAccessCount;

		// Token: 0x04005D05 RID: 23813
		[Tooltip("The linked tasks that also guard a task. If the task guard is not linked against any other tasks it doesn't have much purpose. Marked as LinkedTask to ensure all tasks linked are linked to the same set of tasks")]
		[LinkedTask]
		public TaskGuard[] linkedTaskGuards;

		// Token: 0x04005D06 RID: 23814
		[Tooltip("If true the task will wait until the child task is available. If false then any unavailable child tasks will be skipped over")]
		public SharedBool waitUntilTaskAvailable;

		// Token: 0x04005D07 RID: 23815
		private int executingTasks;

		// Token: 0x04005D08 RID: 23816
		private bool executing;
	}
}
