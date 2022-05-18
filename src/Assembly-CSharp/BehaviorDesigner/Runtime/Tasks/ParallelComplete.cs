using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001494 RID: 5268
	[TaskDescription("Similar to the parallel selector task, except the parallel complete task will return the child status as soon as the child returns success or failure.The child tasks are executed simultaneously.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=136")]
	[TaskIcon("{SkinColor}ParallelCompleteIcon.png")]
	public class ParallelComplete : Composite
	{
		// Token: 0x06007E5E RID: 32350 RVA: 0x000557EC File Offset: 0x000539EC
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x00055804 File Offset: 0x00053A04
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x0005581D File Offset: 0x00053A1D
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x00055825 File Offset: 0x00053A25
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x0005583A File Offset: 0x00053A3A
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x002C8C54 File Offset: 0x002C6E54
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x002C8C84 File Offset: 0x002C6E84
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 2 || this.executionStatus[i] == 1)
				{
					return this.executionStatus[i];
				}
				if (this.executionStatus[i] == null)
				{
					return 2;
				}
			}
			return 3;
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x002C8CD0 File Offset: 0x002C6ED0
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BA5 RID: 27557
		private int currentChildIndex;

		// Token: 0x04006BA6 RID: 27558
		private TaskStatus[] executionStatus;
	}
}
