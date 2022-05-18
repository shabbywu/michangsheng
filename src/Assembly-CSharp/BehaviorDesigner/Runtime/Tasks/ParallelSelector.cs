using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x02001495 RID: 5269
	[TaskDescription("Similar to the selector task, the parallel selector task will return success as soon as a child task returns success. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. If one tasks returns success the parallel selector task will end all of the child tasks and return success. If every child task returns failure then the parallel selector task will return failure.")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=28")]
	[TaskIcon("{SkinColor}ParallelSelectorIcon.png")]
	public class ParallelSelector : Composite
	{
		// Token: 0x06007E68 RID: 32360 RVA: 0x00055845 File Offset: 0x00053A45
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x0005585D File Offset: 0x00053A5D
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = 3;
		}

		// Token: 0x06007E6A RID: 32362 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06007E6B RID: 32363 RVA: 0x00055876 File Offset: 0x00053A76
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06007E6C RID: 32364 RVA: 0x0005587E File Offset: 0x00053A7E
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06007E6D RID: 32365 RVA: 0x00055893 File Offset: 0x00053A93
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06007E6E RID: 32366 RVA: 0x002C8D00 File Offset: 0x002C6F00
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x002C8D30 File Offset: 0x002C6F30
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == 3)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == 2)
				{
					return 2;
				}
			}
			if (!flag)
			{
				return 3;
			}
			return 1;
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x002C8D74 File Offset: 0x002C6F74
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = 0;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04006BA7 RID: 27559
		private int currentChildIndex;

		// Token: 0x04006BA8 RID: 27560
		private TaskStatus[] executionStatus;
	}
}
