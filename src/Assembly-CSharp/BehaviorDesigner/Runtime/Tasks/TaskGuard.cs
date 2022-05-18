using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020014AF RID: 5295
	[TaskDescription("The task guard task is similar to a semaphore in multithreaded programming. The task guard task is there to ensure a limited resource is not being overused. \n\nFor example, you may place a task guard above a task that plays an animation. Elsewhere within your behavior tree you may also have another task that plays a different animation but uses the same bones for that animation. Because of this you don't want that animation to play twice at the same time. Placing a task guard will let you specify how many times a particular task can be accessed at the same time.\n\nIn the previous animation task example you would specify an access count of 1. With this setup the animation task can be only controlled by one task at a time. If the first task is playing the animation and a second task wants to control the animation as well, it will either have to wait or skip over the task completely.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=40")]
	[TaskIcon("{SkinColor}TaskGuardIcon.png")]
	public class TaskGuard : Decorator
	{
		// Token: 0x06007F13 RID: 32531 RVA: 0x000561CA File Offset: 0x000543CA
		public override bool CanExecute()
		{
			return this.executingTasks < this.maxTaskAccessCount.Value && !this.executing;
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x002C9A04 File Offset: 0x002C7C04
		public override void OnChildStarted()
		{
			this.executingTasks++;
			this.executing = true;
			for (int i = 0; i < this.linkedTaskGuards.Length; i++)
			{
				this.linkedTaskGuards[i].taskExecuting(true);
			}
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x000561EA File Offset: 0x000543EA
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.executing || !this.waitUntilTaskAvailable.Value)
			{
				return status;
			}
			return 3;
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x00056204 File Offset: 0x00054404
		public void taskExecuting(bool increase)
		{
			this.executingTasks += (increase ? 1 : -1);
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x002C9A48 File Offset: 0x002C7C48
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

		// Token: 0x06007F18 RID: 32536 RVA: 0x0005621A File Offset: 0x0005441A
		public override void OnReset()
		{
			this.maxTaskAccessCount = null;
			this.linkedTaskGuards = null;
			this.waitUntilTaskAvailable = true;
		}

		// Token: 0x04006BFC RID: 27644
		[Tooltip("The number of times the child tasks can be accessed by parallel tasks at once")]
		public SharedInt maxTaskAccessCount;

		// Token: 0x04006BFD RID: 27645
		[Tooltip("The linked tasks that also guard a task. If the task guard is not linked against any other tasks it doesn't have much purpose. Marked as LinkedTask to ensure all tasks linked are linked to the same set of tasks")]
		[LinkedTask]
		public TaskGuard[] linkedTaskGuards;

		// Token: 0x04006BFE RID: 27646
		[Tooltip("If true the task will wait until the child task is available. If false then any unavailable child tasks will be skipped over")]
		public SharedBool waitUntilTaskAvailable;

		// Token: 0x04006BFF RID: 27647
		private int executingTasks;

		// Token: 0x04006C00 RID: 27648
		private bool executing;
	}
}
